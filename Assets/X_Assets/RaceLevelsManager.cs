using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class RaceLevelsManager : MonoBehaviour
{
    public static RaceLevelsManager Instance;
    static string _CurrentRaceLevel = "_CurrentRaceLevel";

    [Header("Race Levels")]
    public GameObject[] levels;
    public int raceLevelNo;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        ActiveLevel();
    }

    void ActiveLevel()
    {
        raceLevelNo = CurrentRaceLevel;
        raceLevelNo %= levels.Length;
        levels[raceLevelNo].gameObject.SetActive(true);

        foreach (var pathfollower1 in levels[raceLevelNo].gameObject.GetComponentsInChildren<PathFollower1>())
        {
            pathfollower1.enabled = false;
        }
    }

    public void LoadNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public int CurrentRaceLevel
    {
        get
        {
            return PlayerPrefs.GetInt(_CurrentRaceLevel, 0);
        }
        set
        {
            PlayerPrefs.SetInt(_CurrentRaceLevel, value);
        }
    }
}
