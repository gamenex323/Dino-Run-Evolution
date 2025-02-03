using System.Collections;
using System.Collections.Generic;
using THEBADDEST.InteractSyetem;
using Unity.VisualScripting;
using UnityEngine;

public class InsectIdManager : MonoBehaviour
{
    public static InsectIdManager instance;
    public static InsectID[] insectIds;
    public TriggerCheck[] aiCollider;

    private void Awake()
    {
        instance = this;
        insectIds = GetComponentsInChildren<InsectID>();
    }

    [SerializeField] int[] HaveTheseNumbers;
    int reqNumber = -1;

    public void SetNumber()
    {
        int num = (int)ReferenceManager.instance.characterController.currentLevel;


        for (int i = 0; i < HaveTheseNumbers.Length; i++)
        {
            if (num == HaveTheseNumbers[i])
            {
                reqNumber = i;
            }
        }
        if (PlayerPrefs.GetInt("Insect_" + reqNumber + "_Selected", 0) == 1)
        {
            ArrowDetector.instance.gameObject.SetActive(false);
        }
        else
        {
            if (reqNumber == -1)
            {
                ArrowDetector.instance.gameObject.SetActive(false);
            }
            else
            {
                ArrowDetector.instance.target = insectIds[reqNumber].transform;
            }
        }
    }

    int id;

    public int ReturnID(InsectID insectID)
    {
        id = -1;

        for (int i = 0; i < insectIds.Length; i++)
        {
            if (insectIds[i] == insectID)
            {
                id = i;
            }
        }

        return id;
    }

    //public void assignTarget(InsectID insectID)
    //{
    //    int number = ReturnID(insectID);
    //    number++;
    //    ArrowDetector.instance.target = insectIds[number].gameObject.transform;
    //}

    public void Try()
    {
        //if (this.gameObject.GetComponentInChildren<Collectable>().levelCount <= ReferenceManager.instance.characterController.currentLevelCount)
        //{
        //    this.gameObject.GetComponentInChildren<TriggerCheck>().gameObject.GetComponent<SphereCollider>().radius -= 1;
        //}
        GetSphereColliders();
    }

    public void GetSphereColliders()
    {
        aiCollider = GetComponentsInChildren<TriggerCheck>();
        Debug.Log("111");
        for (int i = 0; i < aiCollider.Length; i++)
        {
            Debug.Log("222");
            if (this.gameObject.GetComponentInChildren<Collectable>().levelCount <= ReferenceManager.instance.characterController.currentLevelCount)
            {
                Debug.Log("333");
                aiCollider[i].gameObject.GetComponent<SphereCollider>().radius = aiCollider[i].gameObject.GetComponent<SphereCollider>().radius - 1;
            }
        }
    }
}
