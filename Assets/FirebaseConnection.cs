//using DG.Tweening;
//using Firebase;
//using Firebase.Analytics;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Analytics;
//using UnityEngine.SceneManagement;

//public class FirebaseConnection : MonoBehaviour
//{
//    public static FirebaseConnection instance;

//    private void Awake()
//    {
//        if (instance == null)
//        {
//            instance = this;
//            DontDestroyOnLoad(this.gameObject);
//        }
//    }

//    private void Start()
//    {
//        Invoke("OnStart", 1);
//    }

//    void OnStart()
//    {
//        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
//        {
//            var dependencyStatus = task.Result;
//            if (dependencyStatus == DependencyStatus.Available)
//            {
//                //Debug.Log("FireBase Available");
//                var app = FirebaseApp.DefaultInstance;
//                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
//                FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);
//            }
//            else
//            {
//                Debug.LogError(System.String.Format(
//              "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
//                //.Firebase Unity SDK is not safe to use here.
//                Debug.Log("Firebase Unity SDK is not safe to use here.");
//            }
//        });
//    }

//    public void LevelStarted(int LevelNumber)
//    {
//        string currentlevel = LevelNumber.ToString();
//        print("RE_Level Started" + currentlevel);
//        Parameter[] StoryParameter =
//        {
//         new Parameter("RE_Current_Level", currentlevel)
//        };
//        FirebaseAnalytics.LogEvent("RE_LevelStart_V1", StoryParameter);

//    }

//    public void LevelStartedRace(int LevelNumber)
//    {
//        string currentlevel = LevelNumber.ToString();
//        print("RE_Level Started_Race" + currentlevel);
//        Parameter[] StoryParameter =
//        {
//         new Parameter("RE_Current_Level_Race", currentlevel)
//        };
//        FirebaseAnalytics.LogEvent("RE_LevelStart_Race_V1", StoryParameter);

//    }


//    public void LevelComplete(int LevelNumber)
//    {
//        string LevelCLear = LevelNumber.ToString();
//        print("RE_Level Clear" + LevelCLear);
//        Parameter[] StoryParameter =
//        {
//         new Parameter("RE_Current_Level",LevelCLear)
//                    };
//        FirebaseAnalytics.LogEvent("RE_LevelCleared_V1", StoryParameter);
//    }

//    public void LevelCompleteOpenWorld(int LevelNumber)
//    {
//        string LevelCLearOpenWorld = LevelNumber.ToString();
//        print("RE_Level Clear_Open_World" + LevelCLearOpenWorld);
//        Parameter[] StoryParameter =
//        {
//         new Parameter("RE_Current_Level_Open_World",LevelCLearOpenWorld)
//                    };
//        FirebaseAnalytics.LogEvent("RE_LevelCleared_OpenWorld_V1", StoryParameter);


//    }

//    public void LevelCompleteRace(int LevelNumber)
//    {
//        string LevelClearRace = LevelNumber.ToString();
//        print("RE_Level Clear_Race" + LevelClearRace);
//        Parameter[] StoryParameter =
//        {
//         new Parameter("RE_Current_Level_Race",LevelClearRace)
//                    };
//        FirebaseAnalytics.LogEvent("RE_LevelCleared_Race_V1", StoryParameter);
//    }

//    public void LevelFailRace(int LevelNumber)
//    {
//        string LevelFailedRace = LevelNumber.ToString();
//        print("RE_Level Failed_Race" + LevelFailedRace);
//        Parameter[] StoryParameter =
//        {
//         new Parameter("RE_Current_Level_Race",LevelFailedRace)
//                    };
//        FirebaseAnalytics.LogEvent("RE_LevelFailed_Race_V1", StoryParameter);
//    }

//    public void LevelFail(int LevelNumber)
//    {
//        string LevelFailed = LevelNumber.ToString();
//        print("RE_Level Failed" + LevelFailed);
//        Parameter[] StoryParameter =
//        {
//         new Parameter("RE_Current_Level",LevelFailed)
//                    };
//        FirebaseAnalytics.LogEvent("RE_LevelFailed_V1", StoryParameter);
//    }

//    public void LevelQuit(int LevelNumber)
//    {
//        string LevelQuit = LevelNumber.ToString();

//        FirebaseAnalytics.LogEvent("RE_LevelQuit_" + LevelQuit);
//    }

//    public void LevelBackToOpenWorld(int LevelNumber)
//    {
//        string LevelBackToOpenWorld = LevelNumber.ToString();

//        FirebaseAnalytics.LogEvent("RE_LevelBackToOpenWorld_" + LevelBackToOpenWorld);
//    }


//    #region Firebase Check In Reward Gameplay

//    public void Check_RewardedAdsUpgradeCharacter()
//    {
//        string showMessage = "Reward_Upgrade_Character";
//        FirebaseAnalytics.LogEvent(showMessage);
//        print("Upgrade Character :" + showMessage);
//    }

//    public void Check_RewardedAdsUpgradeCharacterSpeed()
//    {
//        string showMessage = "Reward_Upgrade_Speed";
//        FirebaseAnalytics.LogEvent(showMessage);
//        print("Upgrade Character Speed :" + showMessage);
//    }

//    public void Check_RewardedAdsOnLizard()
//    {
//        string showMessage = "Reward_Lizard";
//        FirebaseAnalytics.LogEvent(showMessage);
//        print("Lizard Reward :" + showMessage);
//    }

//    public void Check_RewardedAdsOnBonusChoser()
//    {
//        string showMessage = "Reward_Bonus_Choser";
//        FirebaseAnalytics.LogEvent(showMessage);
//        print("Bonus Choser :" + showMessage);
//    }

//    public void Check_RewardedAdsOn_InsectScene_Booster()
//    {
//        string showMessage = "Reward_InsectScene_Booster";
//        FirebaseAnalytics.LogEvent(showMessage);
//        print("BoosterInsect :" + showMessage);
//    }

//    public void Check_RewardedAdsOn_RaceScene_Power()
//    {
//        string showMessage = "Reward_Race_Power";
//        FirebaseAnalytics.LogEvent(showMessage);
//        print("PowerRace :" + showMessage);
//    }

//    public void Check_RewardedAdsOn_CharacterUpgradeOnGate()
//    {
//        string showMessage = "Reward_Upgrade_Character_Track";
//        FirebaseAnalytics.LogEvent(showMessage);
//        print("Upgrade Character On Gate :" + showMessage);
//    }

//    public void Check_RewardedAdsOn_ReviveChar()
//    {
//        string showMessage = "Reward_Revive";
//        FirebaseAnalytics.LogEvent(showMessage);
//        print("Revive Character :" + showMessage);
//    }

//    public void Check_RewardedAdsOn_CharacterUpgradeOpenWorld()
//    {
//        string showMessage = "Reward_Upgrade_Character_Open_World";
//        FirebaseAnalytics.LogEvent(showMessage);
//        print("Upgrade Character Open World :" + showMessage);
//    }

//    public void Check_RewardedAdsOn_HeadGearsItem()
//    {
//        string showMessage = "Reward_Headgears";
//        FirebaseAnalytics.LogEvent(showMessage);
//        print("Headgears :" + showMessage);
//    }

//    public void Check_RewardedAdsOn_HeadGearsItemOnStore()
//    {
//        string showMessage = "Reward_Store_Headgears";
//        FirebaseAnalytics.LogEvent(showMessage);
//        print("Store Headgears :" + showMessage);
//    }

//    public void Check_RewardedAdsOn_StoreCash()
//    {
//        string showMessage = "Reward_Store_Cash";
//        FirebaseAnalytics.LogEvent(showMessage);
//        print("Store Cash :" + showMessage);
//    }

//    #endregion
//}
