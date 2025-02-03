using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameAssets.GameSet.GameDevUtils.Managers;
using MergeSystem;
using Sirenix.OdinInspector;
using GalarySystem;
using THEBADDEST.InteractSyetem;
using DG.Tweening;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    public bool isLizardScene;
    [SerializeField] GameObject splashScreen;
    [SerializeField] GameObject gameplayLoadingScreen;
    [SerializeField] GameObject splashLoadingScreen;
    public GameObject OpenWorldSplashScreen;
    public GameObject RaceSplashScreen;
    [SerializeField] GameObject noThanksLizard;
    [SerializeField] GameObject homePanel;


    public GameObject homeOpenWorldPanel;
    [SerializeField] GameObject gamplayPanel;
    public GameObject competePanel;
    public GameObject openWorldCompletePanel;
    public GameObject levelFailPanel;

    public GameObject popUpForLizardScene;
    public GameObject newInsectPanel;
    public GameObject MainCanvus;
    [SerializeField] ParticleSystem particle;
    public Image progressionBar;
    public Image LevelCounterFiller;
    public int levelNo;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject nextButtonlizad;
    [SerializeField] GameObject revivNoThanks;

    [SerializeField] GameObject letsPlayLizard;
    [SerializeField] GameObject letsclaimButton;
    [Header("Text Fields"), SerializeField] Text levelNoText;
    bool switchButton;
    [SerializeField] int buildNumber;

    [FoldoutGroup("PopUp Data")]
    [SerializeField] GameObject specialPopUpItem;
    [FoldoutGroup("PopUp Data")]
    [SerializeField] GameObject noThankx;

    [FoldoutGroup("Tutorial Data")]
    public GameObject Hand;

    [FoldoutGroup("RateUs Data")]

    public int activeSceneIndex;
    public string activeSceneName;

    public bool isLoadingCheck = false;

    public GameObject cageTutorialPanel;
    public RectTransform cageCircle;

    public GameObject boosterTutorialPanel;
    public RectTransform boosterCircle;

    public GameObject hurdleTutorialPanel;
    public RectTransform hurdleCircle;


    public bool isRewardedPlay = false;

    public GameObject[] endPointInsects;
    //public AppRating appRating;

    void Awake()
    {
        GameController.onLevelComplete += OnLevelComplete;
        GameController.onCompleteOpenWorldLevel += OnCompleteOpenWorldLevel;
        GameController.onGameplay += Gameplay;
        GameController.onLevelFail += LevelFail;
        GameController.onLizardLevelFail += LizardLevelFail;
        GameController.onHome += Home;
        GameController.OnRevive += Gameplay;
        Levelprogression.OnCompleteProgression += ButtonStateChange;
        Vibration.Init();
    }

    private void Start()
    {
        //_Sajid_Saingal AdController.instance?.ShowBannerAd(AdController.BannerAdTypes.ADAPTIVE);

        if (CoinsManager.Instance.Coins >= 20 && FirstTime == 0)
        {
            FirstTime = 1;
            Hand.SetActive(true);
        }

        Application.targetFrameRate = 60;

        if (!isLoadingCheck)
        {
            if (splashLoadingScreen)
                splashLoadingScreen.SetActive(true);

            if (gameplayLoadingScreen)
                gameplayLoadingScreen.SetActive(true);
        }


        if (PlayerPrefManager.GetLevelNumber(PlayerPrefManager.GetSceneName()) == 5)
        {
            //appRating.RateAndReview();
        }

        if (!isLoadingCheck)
        {
            //_Sajid_Saingal
            //////if (AdController.instance.splashCounter == 0)
            //////{
            //////    AdController.instance.splashCounter++;
            //////    Invoke(nameof(DisableGamplayLoadingScreen), 5.5f);
            //////}
            //////else
            //////{
            //////    Invoke(nameof(DisableFirstTimeLoadingScreen), 1.25f);
            //////}
        }
    }

    public void OnClickLizardScene()
    {
        SceneManager.LoadScene(2);
    }

    void DisableGamplayLoadingScreen()
    {
        if (splashLoadingScreen)
            splashLoadingScreen.SetActive(false);
    }

    void DisableFirstTimeLoadingScreen()
    {
        if (gameplayLoadingScreen)
            gameplayLoadingScreen.SetActive(false);
    }

    //Events Definations
    void Home()
    {
        ActivePanel(home: true);
    }

    public void OnClickOpenWorldBackButton()
    {
        GalaryManager.SpecialItemUnlock = 0;
        ReferenceManager.instance.galaryManager.UpGradeSpecialItem(0);

        //_Sajid_Saingal  FirebaseConnection.instance?.LevelBackToOpenWorld(levelNo);


        if (OpenWorldSplashScreen) OpenWorldSplashScreen.SetActive(true);

        if (MainCanvus) MainCanvus.SetActive(false);

        SceneManager.LoadScene(1);
    }

    void LevelFail()
    {
        StartCoroutine(DelayONFail());
        //_Sajid_Saingal  FirebaseConnection.instance?.LevelFail(levelNo);
    }

    void LizardLevelFail()
    {
        StartCoroutine(LizardDelay());
    }

    void Gameplay()
    {
        if (ReferenceManager.instance.characterController.cinemachineAnimator)
            ReferenceManager.instance.characterController.cinemachineAnimator.SetActive(true);

        levelNo = PlayerPrefManager.GetLevelNumber(PlayerPrefManager.GetSceneName());
        levelNo += 1;

        Debug.Log("LevelNo :" + levelNo);

        levelNoText.text = $"Level {levelNo.ToString("00")}";
        ActivePanel(gameplay: true);

        if (!isLizardScene)
        {
            if (levelNo == 1)
            {
                for (int i = 0; i < endPointInsects.Length; i++)
                {
                    if (endPointInsects[i])
                        endPointInsects[i].SetActive(false);
                }
            }

            if (levelNo == 2)
            {
                for (int i = 0; i < endPointInsects.Length; i++)
                {
                    if (endPointInsects[i])
                        endPointInsects[i].SetActive(true);
                }
            }
        }


        //_Sajid_Saingal   FirebaseConnection.instance?.LevelStarted(levelNo);

    }

    void OnLevelComplete()
    {
        StartCoroutine(Delay());

        //_Sajid_Saingal  FirebaseConnection.instance?.LevelComplete(levelNo);
    }

    void OnCompleteOpenWorldLevel()
    {
        StartCoroutine(DelayOpenWorld());

        //_Sajid_Saingal  FirebaseConnection.instance?.LevelCompleteOpenWorld(levelNo);
    }

    void ActivePanel(bool gameplay = false, bool home = false, bool complete = false, bool fail = false, bool completeOpenWorld = false)
    {
        gamplayPanel.SetActive(gameplay);
        homePanel.SetActive(home);
        competePanel.SetActive(complete);
        levelFailPanel.SetActive(fail);

        if (openWorldCompletePanel)
            openWorldCompletePanel.SetActive(completeOpenWorld);
    }

    IEnumerator LizardDelay()
    {
        yield return new WaitForSeconds(1f);
        //_Sajid_Saingal AdController.instance?.ShowAd(AdController.AdType.INTERSTITIAL);



        yield return new WaitForSeconds(.3f);

        StartCoroutine(OnLizardFail());

    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);

        //  if (PlayerPrefs.GetInt("RewaedLevelId") !=
        //  levelNo)
        //  {


        if (levelNo > 1)
        {
            //_Sajid_Saingal  AdController.instance?.ShowAd(AdController.AdType.INTERSTITIAL);
        }

        //   }

        yield return new WaitForSeconds(.3f);

        //X if (HomeScreen.instance) HomeScreen.instance.Hidebanner();
        //X  if (HomeScreen.instance) HomeScreen.instance.ShowLargeBanner();

        StartCoroutine(OnComplete());

    }

    IEnumerator DelayOpenWorld()
    {
        GalaryManager.SpecialItemUnlock = 0;
        ReferenceManager.instance.galaryManager.UpGradeSpecialItem(0);

        yield return new WaitForSeconds(1f);

        // if (PlayerPrefs.GetInt("RewaedLevelId", levelNo) !=
        //  levelNo)
        // {

        if (levelNo > 3)
        {
            //_Sajid_Saingal AdController.instance?.ShowAd(AdController.AdType.INTERSTITIAL);
        }

        // }

        yield return new WaitForSeconds(.3f);

        LevelCounter += 1;

        if (LevelCounter >= 3 && isLizardScene == false)
        {
            if (specialPopUpItem)
                specialPopUpItem.SetActive(true);

            SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClip, 0.5f);
            Vibration.VibrateNope();

            yield return new WaitForSeconds(3);
            LevelCounter = 0;

            if (noThankx)
                noThankx.SetActive(true);
        }
        else
        {
            StartCoroutine(OnCompleteOpenWorld());
        }

    }

    IEnumerator OnComplete()
    {
        if (isLizardScene)
        {
            ActivePanel(complete: true);
            yield return new WaitForSeconds(.5f);
        }
        else
        {
            if (ReferenceManager.instance.levelManager.levelNo + 1 % 4 == 0)
            {
                yield return new WaitForSeconds(.2f);
            }
            else
            {
                ActivePanel(complete: true);
                yield return new WaitForSeconds(.5f);
            }
        }

        if (!isLizardScene)
        {
            Vibration.VibratePop();
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.progression, 0.5f);
            ReferenceManager.instance.levelprogression.UpdateProgressionBar();
        }
        else
        {
            ReferenceManager.instance.characterController.win.Play();
        }

        yield return new WaitForSeconds(3f);

        if (Boss.OneTime == 1)
        {
            NewInsectPanel();
            Boss.OneTime = 0;
        }
        else
        {
            if (nextButton) nextButton.SetActive(true);
        }
    }

    IEnumerator OnLizardFail()
    {
        if (isLizardScene)
        {
            ActivePanel(fail: true);
            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator OnCompleteOpenWorld()
    {
        yield return new WaitForSeconds(0.2f);

        ReferenceManager.instance.characterController.openWorldCharacters.SetActive(false);
        ReferenceManager.instance.characterController.offAllAi.SetActive(false);

        ActivePanel(completeOpenWorld: true);
    }

    IEnumerator DelayONFail()
    {
        if (ReferenceManager.instance.uIManager)
            ReferenceManager.instance.uIManager.hurdleTutorialPanel.SetActive(false);

        if (ReferenceManager.instance.uIManager)
            ReferenceManager.instance.uIManager.boosterTutorialPanel.SetActive(false);

        if (ReferenceManager.instance.uIManager)
            ReferenceManager.instance.uIManager.cageTutorialPanel.SetActive(false);


        yield return new WaitForSeconds(1.7f);
        //_Sajid_Saingal  AdController.instance?.ShowAd(AdController.AdType.INTERSTITIAL);


        yield return new WaitForSeconds(0.3f);


        ActivePanel(fail: true);
    }

    public void CompletePanel()
    {
        DisablePopPanel();
    }

    public void DisablePopPanel()
    {
        specialPopUpItem.SetActive(false);
        StartCoroutine(OnCompleteOpenWorld());
        GalaryManager.TotalSpecialItemUnlock++;
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClip, 0.5f);
        Vibration.VibrateNope();
    }

    public void TapToPlay()
    {
        GameController.changeGameState.Invoke(GameState.Gameplay);
    }

    public void RewardedAdsCharacterSpeed()
    {
        //_Sajid_Saingal  AdController.instance?.ShowRewarded(AddCashSpeedUpgrade);
    }

    public void RewardedAdsCharacter()
    {
        //_Sajid_Saingal  AdController.instance?.ShowRewarded(AddCashCharacterUpgrade);
    }

    void AddCashSpeedUpgrade()
    {

        //_Sajid_Saingal  FirebaseConnection.instance?.Check_RewardedAdsUpgradeCharacterSpeed();

        CoinsManager.Instance.AddCoins(UpgardeManager.instace.speedPrice);
        CoinsManager.Instance.AddCoinOnGamePlay();
        UpgardeManager.instace.InitialCash();
    }

    void RewardedAdsLizard()
    {

        //_Sajid_Saingal  FirebaseConnection.instance?.Check_RewardedAdsOnLizard();

        CoinsManager.Instance.AddCoins(UpgardeManager.instace.speedPrice);
        CoinsManager.Instance.AddCoinOnGamePlay();
        UpgardeManager.instace.InitialCash();
    }

    void AddCashCharacterUpgrade()
    {

        //_Sajid_Saingal   FirebaseConnection.instance?.Check_RewardedAdsUpgradeCharacter();

        CoinsManager.Instance.AddCoins(PlayerPrefs.GetInt("UpgardePrice"));
        CoinsManager.Instance.AddCoinOnGamePlay();
        UpgardeManager.instace.InitialCash();
    }

    public void RewardedAdsOfLizard()
    {
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClip, 0.5f);
        Vibration.VibrateNope();
        StartCoroutine(DelayForLizard());
    }

    public void PopUpLizardPanel()
    {
        Vibration.VibrateNope();
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClip, 0.5f);
        //X  if (HomeScreen.instance) HomeScreen.instance.HideLargeBanner();
        if (popUpForLizardScene) popUpForLizardScene.SetActive(true);
        if (MainCanvus) MainCanvus.SetActive(false);
        FunctionTimer.Create(() => { if (noThanksLizard) noThanksLizard.SetActive(true); }, 3);
    }

    public void NewInsectPanel()
    {
        Vibration.VibrateNope();
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClip, 0.5f);

        if (newInsectPanel) newInsectPanel.SetActive(true);
        if (MainCanvus) MainCanvus.SetActive(false);
    }

    IEnumerator DelayForLizard()
    {
        letsclaimButton.SetActive(false);
        //_Sajid_Saingal  AdController.instance?.ShowRewarded(RewardedAdsLizard);

        yield return new WaitForSeconds(.3f);
        if (particle) particle.Play();
        if (letsPlayLizard) letsPlayLizard.SetActive(true);
        Vibration.VibrateNope();
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.upGrade, 0.5f);
    }

    public void ShowSplash()
    {
        GalaryManager.SpecialItemUnlock = 0;
        ReferenceManager.instance.galaryManager.UpGradeSpecialItem(0);

        SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClip, 0.5f);
        Vibration.VibrateNope();
        if (popUpForLizardScene) popUpForLizardScene.SetActive(false);
        if (splashScreen) splashScreen.SetActive(true);
    }

    int BanerCount
    {
        get { return PlayerPrefs.GetInt("BanerCount"); }
        set { PlayerPrefs.SetInt("BanerCount", value); }
    }

    public void ChangeButton()
    {
        if (switchButton)
        {

            if (nextButton) nextButton.SetActive(false);
            if (nextButtonlizad) nextButtonlizad.SetActive(true);
        }
        else
        {
            if (nextButton) nextButton.SetActive(true);
            if (nextButtonlizad) nextButtonlizad.SetActive(false);
        }
    }

    public void PrivacyButton()
    {
        Application.OpenURL("http://dynamactivesol.com/privacy.html");
    }

    //////public void PrivacyPolicy()
    //////{
    //////    Application.OpenURL("https://amgamestudio.com/App/Privacy");
    //////}

    void ButtonStateChange()
    {
        switchButton = true;
        if (nextButton) nextButton.SetActive(false);
        PopUpLizardPanel();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        if (particle) particle.Play();
    }

    public int LevelCounter
    {
        get { return PlayerPrefs.GetInt("LevelCounter"); }
        set { PlayerPrefs.SetInt("LevelCounter", value); }
    }

    int FirstTime
    {
        get { return PlayerPrefs.GetInt("FirstTime"); }
        set { PlayerPrefs.SetInt("FirstTime", value); }
    }
}
