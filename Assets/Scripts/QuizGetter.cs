using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;

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

    public List<Quizes> quizList = new List<Quizes>();
    public class Quizes
    {
        public int type;
        public string text;
        public string answer_txt;
        public int answer_num;
    }
    /*
    public class QuizArray
    {
        //public int count;
        public List<Quizes> quizes;
    }*/

    [Serializable]
    public class QuizDatas
    {
        public string type;

    }
    void Awake()
    {
        quizUrl = "http://192.168.179.5/quiz/index.php";
        StartCoroutine(RequestQuizOne());
    }


    public IEnumerator RequestQuizOne()
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

    public IEnumerator RequestQuizes()
    {
        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("quiz_count", 5);
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
            //string[] quizArray = LitJson.JsonMapper.ToObject<string[]>(result.text);
            var quizArray = LitJson.JsonMapper.ToObject<string[]>(result.text);
            Debug.Log(quizArray[0]);
            foreach(string quiz in quizArray)
            {
                Quizes tmp = JsonUtility.FromJson<Quizes>(quiz);
                quizList.Add(tmp);
            }
            
        }
    }

    public void testRequestQuizes()
    {
        StartCoroutine(RequestQuizes());
    }
}
