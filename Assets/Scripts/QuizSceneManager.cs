using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    void Awake()
    {
        Instance = this;
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
                StartAction();
                break;
            case GameState.Prepare:
                break;

        }
    }
    
	// Use this for initialization
	void StartAction() {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
