using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UserManager : MonoBehaviour{

    public string url;

    [Serializable]
    public class UserData
    {
        public int id;
        public string name;
        public string add_time;
        public int count_all;
        public int count_collect;
    }
    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(gameObject);
        Debug.Log("unko");
        url = "http://192.168.179.5/quiz/index.php";
    }


    public IEnumerator RequestUserData(Action delegateMethod,int myUserId)
    {
        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("user_id", myUserId);
        wwwForm.AddField("type", "user_data");        
        WWW result = new WWW(url, wwwForm);
        //Debug.Log("test");
        yield return result;

        if (!string.IsNullOrEmpty(result.error))
        {
            Debug.LogError("www Error:" + result.error);
        }
        else
        {
            Debug.Log(result.text);
            UserData userData = JsonUtility.FromJson<UserData>(result.text);
            Debug.Log(userData.id);
            Debug.Log(userData.name);
            Debug.Log(userData.add_time);
            delegateMethod();
        }
    }

    public void TestUserRequest()
    {
        StartCoroutine(RequestUserData(SendUserData, 1));
    }

    public void SendUserData()
    {
        Debug.Log("test");
    }
}
