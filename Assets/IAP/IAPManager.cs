using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IAPManager : MonoBehaviour
{
    public string noads;
    public Button iAPButton;

    private void Start()
    {
        if (NoAds == 1)
        {
            //_Sajid_Saingal AdController.instance?.HideBannerAd(AdController.BannerAdTypes.ADAPTIVE);

            iAPButton.gameObject.SetActive(false);
        }
    }

    public void ABC()
    {
        RemoveAds();
    }

    void RemoveAds()
    {
        NoAds = 1;

        //_Sajid_Saingal AdController.instance?.ShowBannerAd(AdController.BannerAdTypes.ADAPTIVE);

        DOVirtual.DelayedCall(5, () => iAPButton.gameObject.SetActive(false));
    }

    public static int NoAds
    {
        get { return PlayerPrefs.GetInt("NoAds"); }
        set { PlayerPrefs.SetInt("NoAds", value); }
    }
}
