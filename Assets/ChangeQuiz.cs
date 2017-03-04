using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeQuiz : MonoBehaviour {

	public void SceneQuiz(){
		SceneManager.LoadScene ("Quiz");
	}

	public void SceneUserData(){
		SceneManager.LoadScene ("UserData");
	}
}
