using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RacePositioning : MonoBehaviour
{
    [Header("GameObjects")]
    private GameObject startPoint;
    public GameObject endPoint;

    [Header("Float_Values")]
    int bottleIndex;

    private void Awake()
    {
        bottleIndex = PlayerPrefs.GetInt("CurrentBottle");
        DOVirtual.DelayedCall(1f, () => SetStartPoint());
        
    }
    private void Start()
    {
        
        
    }
    void SetStartPoint()
    {
        //startPoint = GameController.Instance.bottles[bottleIndex];
    }

   IEnumerator DelayInPlayerRef()
    {
        yield return new WaitForSeconds(0.01f);
    }
   
}