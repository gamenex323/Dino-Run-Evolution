using DG.Tweening;
using GameAssets.GameSet.GameDevUtils.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagID : MonoBehaviour
{
    int insectID;

    // Replace materials with GameObjects
    public GameObject r_object; // Red GameObject
    public GameObject g_object; // Green GameObject

    void Start()
    {
        insectID = GetComponentInParent<FlagIdManager>().ReturnID(this);
        UpdateMat();
    }

    public void UpdateMat()
    {
        // Check PlayerPrefs for selected status
        bool isSelected = PlayerPrefs.GetInt("Insect_" + insectID + "_Selected", 0) == 1;

        if (isSelected)
        {
            // Activate green object and play animation
            g_object.SetActive(true);
            r_object.SetActive(false);

            GetComponentInChildren<DOTweenAnimation>()?.DOPlay();
        }
        else
        {
            // Activate red object
            g_object.SetActive(false);
            r_object.SetActive(true);
        }
    }
}
