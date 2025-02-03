using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSplinePath : MonoBehaviour
{
    public GameObject resetPath;
    public GameObject adIcon;

    private void Start()
    {
        resetPath.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //_Sajid_Saingal  AdController.instance?.ShowRewarded(RewardEarn);
        }
    }

    void RewardEarn()
    {
        ReferenceManager.instance.levelManager.useRewardedPath = true;
        resetPath.SetActive(true);
        adIcon.SetActive(false);
    }
}
