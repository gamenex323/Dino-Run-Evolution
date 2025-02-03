using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class RaceInsect : MonoBehaviour
{
    public static RaceInsect Instance;
    public static event Action onPlayerAnimationState;
    public List<GameObject> raceInsects = new List<GameObject>();

    [SerializeField] public Transform insectSpawnPoint;

    public GameObject raceSplashPrefeb;

    public GameObject canvasRacePlayer;
    public Text canvasRacePlayerText;
    public int raceInsectCount;

    void Awake()
    {
        Instance = this;
        canvasRacePlayer.SetActive(false);
        raceInsectCount = PlayerPrefs.GetInt("LevelOfCharaInsectScene");
        canvasRacePlayerText.text = "0" + raceInsectCount.ToString();
        raceInsects[raceInsectCount - 1].gameObject.SetActive(true);
        insectSpawnPoint = raceInsects[raceInsectCount].transform;
         raceInsects[raceInsectCount - 1].GetComponentInChildren<Animator>().SetTrigger("Walk");

        // raceInsects[raceInsectCount - 1].GetComponentInChildren<Animator>().SetTrigger("Idle");
    }

    // Start is called before the first frame update
    void Start()
    {
        Transform parentTransform = AIUpdateText.Instance.racePlayer.transform;
        raceInsects[raceInsectCount - 1].transform.SetParent(parentTransform);
        RacePlayerControler.instance.Child = raceInsects[raceInsectCount - 1].transform;

        Transform parentTransform1 = raceInsects[raceInsectCount - 1].transform;
        Transform childTransform = AIUpdateText.Instance.raceinsectCanvas.transform;
        childTransform.SetParent(parentTransform1);

        AIUpdateText.Instance.raceinsectCanvas.transform.localRotation = Quaternion.identity;
        AIUpdateText.Instance.raceinsectCanvas.transform.localPosition = new Vector3(0.5f, 1.25f, 0f);

        AIUpdateText.Instance.raceinsectMesh.SetActive(false);
    }

    public void PlayerAnimationState()
    {
        Debug.Log("Walk");
       // raceInsects[raceInsectCount - 1].GetComponentInChildren<Animator>().SetTrigger("Walk");
        Debug.Log("Walk_1");
    }
}
