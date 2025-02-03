using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using THEBADDEST.CharacterController3;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public ParticleSystem racePlayerBoostPar;
    public GameObject loading;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        loading.SetActive(false);
    }

    public void Restart()
    {
        loading.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CompleteRestart()
    {
        loading.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void TapToStart()
    {
        RaceGameController.changeGameStateRace(GameStateRace.GameplayRace);
    }
}
