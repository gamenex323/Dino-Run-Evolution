using SWS;
using System;
using UnityEngine;
using DG.Tweening;

[Serializable]
public struct LevelInfo
{
    public Transform levelData;
    public PathManager path;
    public PathManager rewardedPath;
    public splineMove splineMove;
    public Color fogColor;
    public Material skybox;
}

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string resourcePath;
    [SerializeField] private LevelInfo[] levels;
    [HideInInspector] public LevelInfo currentLevel;
    public int levelNo;
    public int lvl;
    [SerializeField] private bool isTesting;
    public GameObject loadlevel;
    public Transform endPoint;

    public bool useRewardedPath;

    public splineMove splineMove;

    private bool lastPathState;

    private void Awake()
    {
        ActiveLevel();
        GameController.onLevelComplete += OnLevelComplete;
    }

    private void Start()
    {
        //Debug.Log("Pref_Level :" + levelNo);

        if (levelNo > 4)
        {
            if (endPoint)
                endPoint.gameObject.SetActive(true);
        }
        else
        {
            if (endPoint)
                endPoint.gameObject.SetActive(false);
        }

        lastPathState = useRewardedPath;
        UpdatePlayerPath();
    }

    private void Update()
    {
        if (useRewardedPath != lastPathState)
        {
            lastPathState = useRewardedPath;
            UpdatePlayerPath();
        }
    }

    private void OnLevelComplete()
    {
        PlayerPrefManager.SetLevelNumber(PlayerPrefManager.GetSceneName());
    }

    private void ActiveLevel()
    {
        levelNo = PlayerPrefManager.GetLevelNumber(PlayerPrefManager.GetSceneName());

        if (isTesting)
        {
            levelNo = lvl;
        }
        if (levelNo > levels.Length - 1)
        {
            levelNo = UnityEngine.Random.Range(3, levels.Length - 1);
        }

        currentLevel = levels[levelNo];
        currentLevel.levelData.gameObject.SetActive(true);
        loadlevel = Instantiate(Resources.Load(resourcePath + levelNo.ToString(), typeof(GameObject))) as GameObject;
        levels[levelNo].path = loadlevel.GetComponentInChildren<PathManager>();

        GameObject rewardedPathObject = GameObject.FindGameObjectWithTag("RewardedPath");
        if (rewardedPathObject != null)
        {
            levels[levelNo].rewardedPath = rewardedPathObject.GetComponent<PathManager>();
        }

        loadlevel.transform.parent = currentLevel.levelData.transform;

        UpdatePlayerPath();

        if (levelNo > 4)
        {
            if (loadlevel.transform.TryGetComponent<LevelObjectsPlacer>(out LevelObjectsPlacer levelObjectsPlacer))
            {
                if (levelObjectsPlacer.endPointAttachedTarget)
                    endPoint.position = levelObjectsPlacer.endPointAttachedTarget.position;
            }
        }

        RenderSettings.fogColor = levels[levelNo].fogColor;
        RenderSettings.skybox = levels[levelNo].skybox;
    }

    private void UpdatePlayerPath()
    {
        if (useRewardedPath && levels[levelNo].rewardedPath != null)
        {
            SwitchToNewPath();
        }
        if (!useRewardedPath && levels[levelNo].path != null)
        {
            currentLevel.splineMove.pathContainer = levels[levelNo].path;
        }
    }

    public void SwitchToNewPath()
    {
        if (splineMove != null && levels[levelNo].rewardedPath != null)
        {
            ReferenceManager.instance.characterController.StartOnRewardedPathClampBehaviours();
            splineMove.SetPath(levels[levelNo].rewardedPath);
        }
    }
}
