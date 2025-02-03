using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progress_Bar : MonoBehaviour
{
    public static Progress_Bar Instance;

    [Header("GameObjects")]
    public GameObject endPoint;

    [Header("UI references :")]
    [SerializeField] public Image uiFillImage;
    [SerializeField] private Text uiStartText;
    [SerializeField] private Text uiEndText;

    float totalDistance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        endPoint = GameObject.FindWithTag("FillerEndPoint");

        uiStartText.text = (ReferenceManager.instance.levelManager.levelNo + 1f).ToString();
        uiEndText.text = (ReferenceManager.instance.levelManager.levelNo + 2f).ToString();

        NowSetTheData();
    }

    void NowSetTheData()
    {
        totalDistance = Vector3.Distance(ReferenceManager.instance.characterController.transform.position, endPoint.transform.position);
        //Debug.Log("Total_Distance :" + totalDistance);
    }

    private void Update()
    {
        CalculatePosition();
    }

    void CalculatePosition()
    {
        if (ReferenceManager.instance.characterController != null)
        {
            float playerDistance = Vector3.Distance(ReferenceManager.instance.characterController.transform.position, endPoint.transform.position);
            float PlayerCurrentDistance = totalDistance - playerDistance;
            uiFillImage.fillAmount = 0f + (PlayerCurrentDistance / totalDistance);
        }
    }
}