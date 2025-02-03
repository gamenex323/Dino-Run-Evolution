using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    Home,
    Gameplay,
    Complete,
    CompleteOpenWorld,
    Fail,
    Revive,
    OnLizardLevelFail
}

public class GameController : MonoBehaviour
{
    public static GameState gameState = GameState.Home;
    public static Action<GameState> changeGameState;
    public static event Action onHome, onGameplay, onLevelComplete, onLevelFail, onLizardLevelFail, OnRevive, onCompleteOpenWorldLevel;

    void Awake()
    {
        changeGameState += ChangeGameState;
    }

    private void Start()
    {
        changeGameState?.Invoke(GameState.Home);
    }

    void ChangeGameState(GameState state)
    {
        gameState = state;
        switch (gameState)
        {
            case GameState.Home:
                onHome?.Invoke();
                break;
            case GameState.Gameplay:
                onGameplay?.Invoke();
                break;

            case GameState.Complete:
                onLevelComplete?.Invoke();
                break;
            case GameState.CompleteOpenWorld:
                onCompleteOpenWorldLevel?.Invoke();
                break;
            case GameState.Revive:
                OnRevive?.Invoke();
                break;

            case GameState.OnLizardLevelFail:
                onLizardLevelFail?.Invoke();
                break;

            case GameState.Fail:
                {
                    onLevelFail?.Invoke();
                    break;
                }
        }
    }

    IEnumerator DelayInNativeBanner()
    {
        yield return new WaitForSeconds(0.02f);
    }

    void OnDestroy()
    {
        onLevelComplete = null;
        changeGameState = null;
        onLevelFail = null;
        onGameplay = null;
        onHome = null;
        OnRevive = null;
        onCompleteOpenWorldLevel = null;
        onLizardLevelFail = null;
    }
}