using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeSplashScreen : MonoBehaviour
{
    public static HomeSplashScreen Instance;
    public int sceneToLoad1;
    public int sceneTounLoad1;
    public Image Loading1;
    [SerializeField] Text text;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        Loading1.fillAmount = 0;
        StartCoroutine(StartLoad());
    }

    float minTimeToComplete = 1.2f;
    AsyncOperation operation;

    public IEnumerator StartLoad()
    {
        float t = 0;

        while (t < minTimeToComplete)
        {
            t += Time.deltaTime;
            if (Loading1.fillAmount < 0.85f)
            {
                Loading1.fillAmount = (t / minTimeToComplete);
            }
            yield return null;
        }

        operation = SceneManager.LoadSceneAsync(sceneToLoad1);
        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f)
        {
            yield return null;
        }

        Loading1.fillAmount = 1;
        yield return new WaitForSeconds(1);

        DoneLoading1 = true;
    }

    bool DoneLoading1 = false;

    private void Update()
    {
        if (DoneLoading1)
        {
            operation.allowSceneActivation = true;
            SceneManager.UnloadSceneAsync(sceneTounLoad1);
            DoneLoading1 = false;
        }
    }
}
