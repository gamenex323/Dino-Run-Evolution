using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    public static SplashScreen Instance;
    public int sceneToLoad;
    public int sceneTounLoad;
    public Image Loading;
    [SerializeField] Text text;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        Loading.fillAmount = 0;
        StartCoroutine(StartLoad());
    }

    float minTimeToComplete = 2.5f;
    AsyncOperation operation;

    public IEnumerator StartLoad()
    {
        float t = 0;

        while (t < minTimeToComplete)
        {
            t += Time.deltaTime;
            if (Loading.fillAmount < 0.85f)
            {
                Loading.fillAmount = (t / minTimeToComplete);
            }
            yield return null;
        }

        operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f)
        {
            yield return null;
        }

        Loading.fillAmount = 1;
        yield return new WaitForSeconds(1);

        DoneLoading = true;
    }

    bool DoneLoading = false;

    private void Update()
    {
        if (DoneLoading)
        {
            operation.allowSceneActivation = true;
            SceneManager.UnloadSceneAsync(sceneTounLoad);
            DoneLoading = false;
        }
    }

    public static int CutSceneCount
    {
        get { return PlayerPrefs.GetInt("CutScene"); }
        set { PlayerPrefs.SetInt("CutScene", value); }
    }
}
