using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using PathCreation.Examples;
using static Cinemachine.DocumentationSortingAttribute;

public class RaceUIManager : MonoBehaviour
{
    public static RaceUIManager Instance;
    public GameObject homeRacePanel;
    public GameObject raceCompletePanel;
    public GameObject raceFailPanel;
    public int CurrentRaceLevelNo;

    void Awake()
    {
        Instance = this;
        RaceGameController.onHomeRace += HomeRace;
        RaceGameController.onGameplayRace += GameplayRace;
        RaceGameController.onCompleteRaceLevel += OnCompleteRaceLevel;
        RaceGameController.onLevelFailRace += OnFailRace;
    }


    void HomeRace()
    {
        ActivePanel(homeRace: true);
    }

    void GameplayRace()
    {
        ActivePanel(homeRace: false);
        if (AIUpdateText.Instance) AIUpdateText.Instance.SetAiWalkStae();

        foreach (var pathfollower1 in RaceLevelsManager.Instance.levels[RaceLevelsManager.Instance.raceLevelNo].gameObject.
            GetComponentsInChildren<PathFollower1>())
        {
            pathfollower1.enabled = true;
        }


        //_Sajid_Saingal  FirebaseConnection.instance?.LevelStartedRace(RaceLevelsManager.Instance.CurrentRaceLevel + 1);

    }


    void ActivePanel(bool homeRace = false, bool failRace = false, bool completeRace = false)
    {
        if (homeRacePanel)
            homeRacePanel.SetActive(homeRace);

        if (raceCompletePanel)
            raceCompletePanel.SetActive(completeRace);

        if (raceFailPanel)
            raceFailPanel.SetActive(failRace);
    }

    void OnCompleteRaceLevel()
    {
        StartCoroutine(DelayRace());
    }

    IEnumerator DelayRace()
    {
        //_Sajid_Saingal FirebaseConnection.instance?.LevelCompleteRace(RaceLevelsManager.Instance.CurrentRaceLevel + 1);

        RaceLevelsManager.Instance.raceLevelNo = RaceLevelsManager.Instance.CurrentRaceLevel;
        RaceLevelsManager.Instance.raceLevelNo++;
        RaceLevelsManager.Instance.CurrentRaceLevel = RaceLevelsManager.Instance.raceLevelNo;

        yield return new WaitForSeconds(1f);

        //_Sajid_Saingal AdController.instance?.ShowAd(AdController.AdType.INTERSTITIAL);

        yield return new WaitForSeconds(.3f);

        StartCoroutine(OnCompleteRace());
    }

    IEnumerator OnCompleteRace()
    {
        yield return new WaitForSeconds(0.2f);

        ActivePanel(completeRace: true);
    }

    void OnFailRace()
    {
        StartCoroutine(DelayOnFailRace());

        //_Sajid_Saingal FirebaseConnection.instance?.LevelFailRace(RaceLevelsManager.Instance.CurrentRaceLevel + 1);
    }

    IEnumerator DelayOnFailRace()
    {
        yield return new WaitForSeconds(1.7f);
        //_Sajid_Saingal AdController.instance?.ShowAd(AdController.AdType.INTERSTITIAL);


        yield return new WaitForSeconds(0.3f);
        ActivePanel(failRace: true);
    }
}
