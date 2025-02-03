using DG.Tweening;
using GalarySystem;
using GameAssets.GameSet.GameDevUtils.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] GameObject nextbutton;
    [SerializeField] GameObject noThanksbutton;
    [SerializeField] GameObject claimbutton;

    public void LoadScene()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            return;
        }

        SceneManager.LoadSceneAsync(sceneName);
    }

    public void ReloadScene()
    {
        GamePanelOnOff();

        if (string.IsNullOrEmpty(sceneName))
        {
            sceneName = SceneManager.GetActiveScene().name;
        }

        DOVirtual.DelayedCall(0.51f, () => SceneManager.LoadSceneAsync(sceneName));

        if (noThanksbutton) noThanksbutton.GetComponent<Button>().enabled = false;
        if (nextbutton) nextbutton.GetComponent<Button>().enabled = false;
        if (claimbutton) claimbutton.GetComponent<Button>().enabled = false;
    }

    public void ReloadSceneLizard()
    {
        GalaryManager.SpecialItemUnlock = 0;
        ReferenceManager.instance.galaryManager.UpGradeSpecialItem(0);

        GamePanelOnOff();

        if (string.IsNullOrEmpty(sceneName))
        {
            sceneName = SceneManager.GetActiveScene().name;
        }

        DOVirtual.DelayedCall(0.51f, () => SceneManager.LoadSceneAsync(sceneName));

        if (noThanksbutton) noThanksbutton.GetComponent<Button>().enabled = false;
        if (nextbutton) nextbutton.GetComponent<Button>().enabled = false;
        if (claimbutton) claimbutton.GetComponent<Button>().enabled = false;
    }

    void GamePanelOnOff()
    {
        if (ReferenceManager.instance.uIManager.isLizardScene == true)
        {
            if (ReferenceManager.instance.uIManager.OpenWorldSplashScreen)
                ReferenceManager.instance.uIManager.OpenWorldSplashScreen.SetActive(true);
        }
        else
        {
            if (ReferenceManager.instance.uIManager.newInsectPanel)
                ReferenceManager.instance.uIManager.newInsectPanel.SetActive(false);

            if (ReferenceManager.instance.uIManager.OpenWorldSplashScreen)
                ReferenceManager.instance.uIManager.OpenWorldSplashScreen.SetActive(true);

            if (ReferenceManager.instance.uIManager.MainCanvus)
                ReferenceManager.instance.uIManager.MainCanvus.SetActive(false);

            if (ReferenceManager.instance.uIManager.popUpForLizardScene)
                ReferenceManager.instance.uIManager.popUpForLizardScene.SetActive(false);
        }
    }

    public void OpenWorldScenarioStart()
    {
        if (ReferenceManager.instance.uIManager.levelNo % 3 == 0 && ReferenceManager.instance.uIManager.isLizardScene == false)
        {
            if (ReferenceManager.instance.uIManager) ReferenceManager.instance.uIManager.RaceSplashScreen.SetActive(true);
        }
        else
        {
            ReferenceManager.instance.characterController.OpenWorldScenario();
        }
    }

    public void BackToOpenWorldArea()
    {
        ReferenceManager.instance.uIManager.OpenWorldSplashScreen.SetActive(true);

        if (ReferenceManager.instance.uIManager.MainCanvus)
            ReferenceManager.instance.uIManager.MainCanvus.SetActive(false);

        if (string.IsNullOrEmpty(sceneName))
        {
            sceneName = SceneManager.GetActiveScene().name;
        }

        DOVirtual.DelayedCall(0.51f, () => SceneManager.LoadSceneAsync(sceneName));

        if (noThanksbutton) noThanksbutton.GetComponent<Button>().enabled = false;
        if (nextbutton) nextbutton.GetComponent<Button>().enabled = false;
        if (claimbutton) claimbutton.GetComponent<Button>().enabled = false;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(.3f);
        ReloadScene();
    }

    IEnumerator DelayInIni()
    {
        yield return new WaitForSeconds(.3f);
        ReloadScene();
    }

    public void AdPlusReload()
    {
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClip, 0.5f);
        StartCoroutine(Delay());
    }

    public void AdIniPlusReload()
    {
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClip, 0.5f);
        StartCoroutine(DelayInIni());
    }
}