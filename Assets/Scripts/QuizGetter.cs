using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuizGetter : MonoBehaviour {
    string quizUrl;

    [Serializable]
    public class QuizData
    {
        public int type;
        public string text;
        public string answer_txt;
        public int answer_num;
    }
    void Awake()
    {
        quizUrl = "http://192.168.179.5/quiz/index.php";
        StartCoroutine(RequestAPI());
    }


    public IEnumerator RequestAPI()
    {
        
            WWWForm wwwForm = new WWWForm();
            wwwForm.AddField("quiz_id", 1);
            wwwForm.AddField("type", "yontaku");        //4択だけど直そう

            WWW result = new WWW(quizUrl, wwwForm);
            //Debug.Log("test");
            yield return result;

            if (!string.IsNullOrEmpty(result.error))
            {
                Debug.LogError("www Error:" + result.error);
            }
            else
            {
            Debug.Log(result.text);
                QuizData quizData = JsonUtility.FromJson<QuizData>(result.text);
                Debug.Log(quizData.type);
                Debug.Log(quizData.answer_txt);
            Debug.Log(quizData.text);

            //taskArray = todo.task.Split('|');
        }
    
}
	
	// Update is called once per frame
	void Update () {
		
	}
}
