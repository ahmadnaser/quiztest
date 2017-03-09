using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum GameState
{
    Start,
    Prepare,
    Wait,
    Answer,
    Result
}

public class QuizSceneManager : MonoBehaviour {
    public static QuizSceneManager Instance;

    private GameState currentGameState;

    //クイズの個数　どこかで入力させる
    int quizCount = 5;

    GameObject QuizManager;
    QuizUIManager quizUIManager;
    QuizGetter quizGetter;
    /*
    GameObject quizTextPanel;
    GameObject Button1;
    GameObject Button2;
    GameObject Button3;
    GameObject Button4;
    */
    bool hasAnswered;
    int usersAnswerNum;

    float maxTime;


    int quizTurn;

    List<Quizes> quizes = new List<Quizes>();
    public class Quizes
    {
        public int type;
        public string text;
        public string answer_txt;
        public int answer_num;
    }

    List<bool> quizResults = new List<bool>();

    void Awake()
    {
        Instance = this;
        QuizManager = GameObject.Find("QuizManager");
        quizUIManager = gameObject.GetComponent<QuizUIManager>();
        quizGetter = QuizManager.GetComponent<QuizGetter>();

        SetCurrentState(GameState.Start);
    }

    public void SetCurrentState(GameState state)
    {
        currentGameState = state;
        OnGameStateChanged(currentGameState);
    }

    void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                StartCoroutine(StartAction());
                break;
            case GameState.Prepare:
                StartCoroutine(PrepareAction());
                break;
            case GameState.Wait:
                StartCoroutine(WaitAction());
                break;
            case GameState.Answer:
                AnswerAction();
                break;
            case GameState.Result:
                int countTrue = quizResults.Count(boolean => boolean == true);
                Debug.Log("正解数：" + countTrue);
                break;

        }
    }
    
	//startは呼び出し1回のみ　終わったらprepare
	IEnumerator StartAction() {
        //クイズを受信するコルーチンを呼び出す　終わったらリストをコピーする
        yield return StartCoroutine(quizGetter.RequestQuizes(SaveReceivedQuizes,quizCount));
        quizTurn = -1;      //ここ汚い
        //とりあえず時間のマックスは10秒に設定
        maxTime = 10f;
        quizUIManager.GetUIPanel(maxTime);
        SetCurrentState(GameState.Prepare);
	}
	
    //prepare：だいたいここの状態を通る
    //クイズをUIに
    IEnumerator PrepareAction()
    {
        quizTurn++;
        yield return new WaitForSeconds(1);
        quizUIManager.SetQuizOnPanel(quizes, quizTurn);
        SetCurrentState(GameState.Wait);
    }

    //wait：ユーザーのボタン入力を待つ
    IEnumerator WaitAction()
    {
        //ユーザーの入力した答えの番号を0(=未入力)にして入力待ちの状態にする
        usersAnswerNum = 0;
        maxTime = 20f;
        yield return StartCoroutine(quizUIManager.TimerAndButtonCoroutine(maxTime));

        if (usersAnswerNum != 0) Debug.Log("Button:" + usersAnswerNum);
        else Debug.Log("time UP");      //テストでもanswerに飛ばないように分岐するべき

        SetCurrentState(GameState.Answer);
    }

    void AnswerAction()
    {
        if(usersAnswerNum == quizes[quizTurn].answer_num)
        {
            Debug.Log("right!");
            quizResults.Add(true);
        }else
        {
            Debug.Log("wrong!");
            quizResults.Add(false);
        }

        //クイズが最後だったら
        if ((quizTurn + 1) == quizCount)
        {
            SetCurrentState(GameState.Result);
        }
        else
        {
            SetCurrentState(GameState.Prepare);
        }

    }
    //コルーチンの結果をコールバックで受け取ってquizesに保存する
    public void SaveReceivedQuizes(List<QuizGetter.Quizes> receivedQuizes)
    {
        //値のコピー　たぶんもっと簡単に書ける
        foreach(QuizGetter.Quizes quiz in receivedQuizes)
        {
            Quizes tmp = new Quizes();
            tmp.type = quiz.type;
            tmp.text = quiz.text;
            tmp.answer_txt = quiz.answer_txt;
            tmp.answer_num = quiz.answer_num;
            quizes.Add(tmp);
        }
        Debug.Log(quizes[1].text);
        Debug.Log(quizes[1].answer_txt);
        Debug.Log(quizes[1].answer_num);
    }


    //ボタンの入力を受け付けるメソッド
    //ボタン入力を待つメソッド(コルーチン)はUIManagerへ　ちゃんと設計しろ
    public void ButtonClicked(int answerNum)
    {
        if (currentGameState == GameState.Wait)
        {
            usersAnswerNum = answerNum;
            quizUIManager.hasAnswered = true;
        }
    }


}
