using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UnityEngine;

public class PlayerPowerRace : MonoBehaviour
{
    [SerializeField] public RacePlayerHurdle[] racePlayerHurdle;
    [SerializeField] public GameObject playerPowerTool;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RacePlayer"))
        {
            if (other.gameObject.GetComponentInParent<RacePlayer>())
            {
                //_Sajid_Saingal  AdController.instance?.ShowRewarded(OnPlayerPowerReward);
            }
        }
    }

    void OnPlayerPowerReward()
    {
        for (int i = 0; i < racePlayerHurdle.Length; i++)
        {
            Time.timeScale = 1.45f;
            AIUpdateText.Instance.racePlayer.GetComponent<PathFollower1>().currentSpeed = 33;
            racePlayerHurdle[i].raceHurdle.GetComponentInChildren<Collider>().enabled = false;
            playerPowerTool.SetActive(false);
        }


        //_Sajid_Saingal FirebaseConnection.instance?.Check_RewardedAdsOn_RaceScene_Power();
    }
}

[Serializable]
public struct RacePlayerHurdle
{
    public GameObject raceHurdle;
}
