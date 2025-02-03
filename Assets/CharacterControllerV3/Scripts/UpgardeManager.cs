using DG.Tweening;
using GalarySystem;
using GameAssets.GameSet.GameDevUtils.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgardeManager : MonoBehaviour
{
    public static UpgardeManager instace;
    public bool upGradeReward;

    [SerializeField] GameObject adButtonOfCharacter;
    [SerializeField] GameObject adButtonOfSpeed;
    [SerializeField] GameObject cashSpriteCharacter;
    [SerializeField] GameObject cashSpritespeed;
    /// <summary>
    /// Character
    /// </summary>
    public Text levelText;
    public Text levelTextDark;
    [SerializeField] int charcterPrice;
    public Text priceTextOfCharacter;
    public Text priceTextOfCharacterDark;
    [SerializeField] GameObject characterDarkUi;
    [SerializeField] GameObject characterUi;
    /// <summary>
    /// Speed
    /// </summary>
    public Text priceTextOfSpeed;
    public Text levelTextofSpeed;
    public float speed;
    [SerializeField] public int speedPrice;
    public int speedLevel;
    [SerializeField] GameObject speedUi;
    public static event Action<int> onCharacterUpGrade;
    /// <summary>
    /// CharacterController
    /// </summary>
    public THEBADDEST.CharacterController3.CharacterController characterController;

    private void Awake()
    {
        if (instace == null)
        {
            instace = this;
        }

        if (StartGame == 0)
        {
            StartGame = 1;
            UpgradeFactorOfCharacter = 2;
        }

        if (PlayerPrefs.GetInt("LevelOfChara" + PlayerPrefManager.GetSceneName()) == 0)
        {
            if (characterController.currentLevelCount <= 5)
            {
                UpgardeCount = 0;
            }

            UpgardePrice = charcterPrice;

        }
        if (PlayerPrefs.GetInt("SpeedLevel" + PlayerPrefManager.GetSceneName()) == 0)
        {

            characterController.splineMove.speed = speed;
            speedLevel = 1;
            PlayerPrefs.SetFloat("Speed" + PlayerPrefManager.GetSceneName(), speed);
            PlayerPrefs.SetInt("SpeedLevel" + PlayerPrefManager.GetSceneName(), speedLevel);
        }
        else
        {
            speedLevel = PlayerPrefs.GetInt("SpeedLevel" + PlayerPrefManager.GetSceneName());
            speed = PlayerPrefs.GetFloat("Speed" + PlayerPrefManager.GetSceneName());
            characterController.splineMove.speed = speed;
        }
        GalaryManager.onCharacterUpdate += UpDateCaharcterByStore;
    }
    void Start()
    {
        priceTextOfCharacter.text = UpgardePrice.ToString();
        levelText.text = characterController.currentLevelCount.ToString();


        priceTextOfSpeed.text = speedPrice.ToString();
        levelTextofSpeed.text = speedLevel.ToString();
        characterController.splineMove.speed = speed;

        InitialCash();
    }

    public void UpgardeCharcter()
    {
        if (CoinsManager.Instance.Coins >= UpgardePrice)
        {
            ReferenceManager.instance.uIManager.Hand.SetActive(false);
            if (characterController.currentLevelCount == characterController.insects.Count)
            {
                Debug.Log("FinalUp");
            }
            else
            {
                if (characterController.currentLevelCount <= 5)
                {
                    UpgardeCount += 1;
                }

                Vibration.VibrateNope();
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.upGrade, 1);
                characterController.upGradelevelParticle.Play();
                characterController.currentLevelCount++;
                PlayerPrefs.SetInt("LevelOfChara" + PlayerPrefManager.GetSceneName(), characterController.currentLevelCount);
                CoinsManager.Instance.DecCoins(UpgardePrice);
                CoinsManager.Instance.coinUIText.text = CoinsManager.Instance.Coins.ToString();
                CoinsManager.Instance.coinUITextHome.text = CoinsManager.Instance.Coins.ToString();
                levelText.text = characterController.currentLevelCount.ToString();
                characterController.currentLevel = (LevelType)characterController.currentLevelCount;
                characterController.UpgardeLevelOfCharacter(characterController.currentLevel);
                characterUi.GetComponent<DOTweenAnimation>().DORestartById("Pop");
                //Ab  if (AnalyticsMediator.instance) AnalyticsMediator.instance.LogGA_Event("CharacterLevelUpgradeLevel", characterController.currentLevelCount);
                UpGradePrice();
                InitialCash();
                onCharacterUpGrade?.Invoke(characterController.currentLevelCount - 1);
            }
        }
    }

    void UpGradePrice()
    {
        if (characterController.currentLevelCount <= 5)
        {
            if (UpgardeCount == UpgradeFactorOfCharacter)
            {
                UpgardePrice += 5;
                UpgardeCount = 0;
            }
        }
        else
        {
            UpgardePrice += 5;
        }

        priceTextOfCharacter.text = UpgardePrice.ToString();
    }

    void UpDateCaharcterByStore(int id)
    {
        UpGradePrice();
        Vibration.VibrateNope();
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.upGrade, 1);
        characterController.currentLevelCount = id + 1;
        PlayerPrefs.SetInt("LevelOfChara" + PlayerPrefManager.GetSceneName(), characterController.currentLevelCount);
        characterController.currentLevel = (LevelType)characterController.currentLevelCount;
        characterController.UpgardeLevelOfCharacter(characterController.currentLevel);
        onCharacterUpGrade?.Invoke(characterController.currentLevelCount - 1);
    }

    public void UpgardeSpeed()
    {
        if (CoinsManager.Instance.Coins >= speedPrice)
        {
            if (speed == 10)
            {
                //Debug.Log("MaxSpeed");
            }
            else
            {
                Vibration.VibrateNope();
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.upGrade, 1);
                speed += 0.15f;
                characterController.splineMove.speed = speed;
                speedLevel += 1;
                PlayerPrefs.SetInt("SpeedLevel" + PlayerPrefManager.GetSceneName(), speedLevel);
                PlayerPrefs.SetFloat("Speed" + PlayerPrefManager.GetSceneName(), speed);
                CoinsManager.Instance.DecCoins(speedPrice);
                CoinsManager.Instance.coinUIText.text = CoinsManager.Instance.Coins.ToString();
                CoinsManager.Instance.coinUITextHome.text = CoinsManager.Instance.Coins.ToString();
                InitialCash();
                levelTextofSpeed.text = speedLevel.ToString();
                speedUi.GetComponent<DOTweenAnimation>().DORestartById("Pop");

                //Ab  if (AnalyticsMediator.instance) AnalyticsMediator.instance.LogGA_Event("CharacterSpeedUpgradeLevel", speedLevel);
            }
        }
    }

    public void InitialCash()
    {
        CheckCash(UpgardePrice, adButtonOfCharacter, cashSpriteCharacter);
        CheckCash(15, adButtonOfSpeed, cashSpritespeed);
    }

    public void CheckCash(int price, GameObject AdButton, GameObject CashButton)
    {
        if (CoinsManager.Instance.Coins >= price)
        {
            AdButton.SetActive(false);
            CashButton.SetActive(true);
        }
        else
        {
            //AdButton.SetActive(true);
            CashButton.SetActive(true);
        }
    }

    public enum UpgradeType
    {
        Character,
        PuffDelay,
        PuffScaleLimit
    }

    int UpgardeCount
    {
        get { return PlayerPrefs.GetInt("UpgardeCount"); }
        set { PlayerPrefs.SetInt("UpgardeCount", value); }
    }

    public static int UpgardePrice
    {
        get { return PlayerPrefs.GetInt("UpgardePrice"); }
        set { PlayerPrefs.SetInt("UpgardePrice", value); }
    }

    public static int UpgradeFactorOfCharacter
    {
        get { return PlayerPrefs.GetInt("UpgradeFactorOfCharacternew"); }
        set { PlayerPrefs.SetInt("UpgradeFactorOfCharacternew", value); }
    }

    int StartGame
    {
        get { return PlayerPrefs.GetInt("StartGame1"); }
        set { PlayerPrefs.SetInt("StartGame1", value); }
    }

    private void OnDestroy()
    {
        onCharacterUpGrade = null;
    }
}