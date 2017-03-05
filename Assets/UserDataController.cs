using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserDataController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void UserDataReceived(int userId,string userName,string createtaime,int totalanswer,int correctanswer){
		textmaa (userId);

	}

	//自分の名前のUIを更新するメソッド
	public void textmaa(int userId){
		//gamebjectを取得


		//textに代入
		//text = userId;
}
}
