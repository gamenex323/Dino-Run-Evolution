using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameStateRace
{
    GameplayRace,
    CompleteRace,
    HomeRace,
    FailRace,
    ReviveRace
}

public class RaceGameController : MonoBehaviour
{
    public static GameStateRace gameStateRace = GameStateRace.HomeRace;
    public static Action<GameStateRace> changeGameStateRace;
    public static event Action onGameplayRace, onLevelFailRace, onReviveRace, onHomeRace, onCompleteRaceLevel;

    void Awake()
    {
        changeGameStateRace += ChangeGameStateRace;
    }

    private void Start()
    {
        changeGameStateRace?.Invoke(GameStateRace.HomeRace);
    }

    void ChangeGameStateRace(GameStateRace stateRace)
    {
        gameStateRace = stateRace;
        switch (gameStateRace)
        {
            case GameStateRace.HomeRace:
                onHomeRace?.Invoke();
                break;
            case GameStateRace.GameplayRace:
                onGameplayRace?.Invoke();
                break;
            case GameStateRace.CompleteRace:
                onCompleteRaceLevel?.Invoke();
                break;
            case GameStateRace.ReviveRace:
                onReviveRace?.Invoke();
                break;
            case GameStateRace.FailRace:
                {
                    onLevelFailRace?.Invoke();
                    break;
                }
        }
    }

    void OnDestroy()
    {
        changeGameStateRace = null;
        onLevelFailRace = null;
        onGameplayRace = null;
        onReviveRace = null;
        onCompleteRaceLevel = null;
        onHomeRace = null;
    }
}
