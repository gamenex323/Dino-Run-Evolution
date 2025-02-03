using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BonusChoser : MonoBehaviour
{
    int value;
    int CollectedCoin;
    int curvalue;
    [SerializeField] Text bonusText; //this used to show how many you got from the chest it located in complete panel
    public GameObject[] AnimateCashOnVRewardedVideo;
    public Transform target;
    public Transform cashInitialPos;

    private void Start()
    {
        CoinsManager.Instance.AddCoinOnGamePlay();
        CollectedCoin = UpgardeManager.instace.characterController.totallCollection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("bonus"))
        {
            collision.GetComponent<DOTweenAnimation>().DORestart();
            value = int.Parse(collision.transform.GetComponentInChildren<Text>().text);
            curvalue = value * CollectedCoin;

            bonusText.text = curvalue.ToString();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<DOTweenAnimation>().DORewind();
    }

    public void StopAnimation()
    {
        CoinsManager.Instance.CollectionValue = curvalue;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<DOTweenAnimation>().DOPause();
        //_Sajid_Saingal  AdController.instance?.ShowRewarded(AddCash);
    }

    void AddCash()
    {
        //_Sajid_Saingal FirebaseConnection.instance?.Check_RewardedAdsOnBonusChoser();


        AnimateCash();
        CoinsManager.Instance.AddCoins(CoinsManager.Instance.CollectionValue);
        CoinsManager.Instance.AddCoinOnGamePlay();
        UpgardeManager.instace.InitialCash();
    }

    public void AnimateCash()
    {
        for (int i = 0; i < AnimateCashOnVRewardedVideo.Length; i++)
        {
            AnimateCashOnVRewardedVideo[i].SetActive(true);

            for (int J = 0; J < AnimateCashOnVRewardedVideo.Length; J++)
            {
                AnimateCashOnVRewardedVideo[0].transform.DOMove(target.transform.position, 0.00001f).SetEase(Ease.OutBounce).OnComplete(delegate
                {
                    AnimateCashOnVRewardedVideo[0].transform.gameObject.SetActive(false);
                    AnimateCashOnVRewardedVideo[0].transform.position = cashInitialPos.position;
                });
                AnimateCashOnVRewardedVideo[1].transform.DOMove(target.transform.position, 0.25f).SetEase(Ease.OutBounce).OnComplete(delegate
                {
                    AnimateCashOnVRewardedVideo[1].transform.gameObject.SetActive(false);
                    AnimateCashOnVRewardedVideo[1].transform.position = cashInitialPos.position;
                });
                AnimateCashOnVRewardedVideo[2].transform.DOMove(target.transform.position, 0.5f).SetEase(Ease.OutBounce).OnComplete(delegate
                {
                    AnimateCashOnVRewardedVideo[2].transform.gameObject.SetActive(false);
                    AnimateCashOnVRewardedVideo[2].transform.position = cashInitialPos.position;
                });
            }
        }
    }
}
