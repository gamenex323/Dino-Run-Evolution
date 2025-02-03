////////////////////////////////////////////////////////////////////////////////
//  
// @author Benoît Freslon @benoitfreslon
// https://github.com/BenoitFreslon/Vibration
// https://benoitfreslon.com
//
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class VibrationExample : MonoBehaviour
{
    public static VibrationExample instance;
    public Text inputTime;
    public Text inputPattern;
    public Text inputRepeat;
    public Text txtAndroidVersion;

    // Use this for initialization
    void Start()
    {
        instance = this;
        //Sajid Saingal  Vibration.Init();
        Debug.Log("Application.isMobilePlatform: " + Application.isMobilePlatform);
        // txtAndroidVersion.text = "Android Version: " + Vibration.AndroidVersion.ToString ();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TapVibrate()
    {
        //Sajid Saingal Vibration.Vibrate();
    }

    public void TapVibrateCustom()
    {
        //Sajid Saingal  Vibration.Vibrate(int.Parse(inputTime.text));
    }

    public void TapVibratePattern()
    {
        string[] patterns = inputPattern.text.Replace(" ", "").Split(',');
        long[] longs = Array.ConvertAll<string, long>(patterns, long.Parse);

        Debug.Log(longs.Length);
        //Vibration.Vibrate ( longs, int.Parse ( inputRepeat.text ) );

        //Sajid Saingal Vibration.Vibrate(longs, int.Parse(inputRepeat.text));
    }

    public void TapCancelVibrate()
    {

        //Sajid Saingal  Vibration.Cancel();
    }

    public void TapPopVibrate()
    {
        //Sajid Saingal  Vibration.VibratePop();
    }

    public void TapPeekVibrate()
    {
        //Sajid Saingal Vibration.VibratePeek();
    }

    public void TapNopeVibrate()
    {
        //Sajid Saingal  Vibration.VibrateNope();
    }
}
