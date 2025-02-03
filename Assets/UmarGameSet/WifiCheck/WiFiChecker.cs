//using DG.Tweening;
//using GameAssets.GameSet.GameDevUtils.Managers;
//using SWS;
//using System.Collections;
//using UnityEngine;
//using UnityEngine.UI;

//public class WiFiChecker : MonoBehaviour
//{
//    public GameObject popup;
//    public GameObject mainCanvus;
//    [SerializeField] splineMove splineMove;
//    public int a;
//    bool canHide;
//    bool canOpen;
//    private void Awake()
//    {
//        canHide = true;
//    }
//    public void Update()
//    {
//        a++;
//        if (a == 100)
//        {
//            //   CheckWiFiStatus();
//            a = 0;
//        }
//    }

//    private void CheckWiFiStatus()
//    {
//        //switch (Application.internetReachability)
//        //{
//        //    case NetworkReachability.NotReachable:
//        //        ShowPopup();
//        //        break;
//        //    case NetworkReachability.ReachableViaCarrierDataNetwork:
//        //        HidePopup();
//        //        break;
//        //    case NetworkReachability.ReachableViaLocalAreaNetwork:
//        //        HidePopup();
//        //        break;
//        //}
//        if (Application.internetReachability == NetworkReachability.NotReachable)
//        {
//            if (!canOpen)
//            {
//                canOpen = true;

//                ShowPopup();
//                splineMove.Pause();
//            }
//        }
//        else
//        {
//            if (!canHide)
//            {
//                canHide = true;

//                HidePopup();
//                if (GameController.gameState == GameState.Gameplay)
//                    splineMove.StartMove();
//            }
//        }
//    }

//    private void ShowPopup()
//    {
//        Debug.Log("NO INTRRNET");
//        StartCoroutine(Delay());
//    }
//    IEnumerator Delay()
//    {
//        yield return new WaitForSeconds(3);
//        popup.SetActive(true);
//        mainCanvus.SetActive(false);
//        SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClip, 0.5f);
//        Vibration.VibrateNope();
//        popup.transform.DOScale(1, 0.7f).SetEase(Ease.OutBounce);
//        canHide = false;
//    }
//    private void HidePopup()
//    {

//        //Time.timeScale = 1;
//        popup.SetActive(false);
//        mainCanvus.SetActive(true);
//        SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClip, 0.5f);
//        Vibration.VibrateNope();
//        //canOpen = false;
//    }

//    public void ConnectToWifi()
//    {
//#if UNITY_EDITOR || UNITY_STANDALONE
//        Debug.Log("Connect to WiFi button clicked.");
//#elif UNITY_IOS
//            UnityEngine.iOS.Device.OpenURL("App-Prefs:root=WIFI");
//#elif UNITY_ANDROID
//            AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
//            AndroidJavaObject wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi");
//            AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent");
//            intent.Call<AndroidJavaObject>("setAction", "android.net.wifi.PICK_WIFI_NETWORK");
//            activity.Call("startActivity", intent);
//#endif



//    }

//}
