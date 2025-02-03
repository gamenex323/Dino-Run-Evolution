using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialItem : MonoBehaviour
{
    [SerializeField] GameObject[] items;
    public void SetItemOnCHaracter(int index)
    {

        foreach (var item in items)
        {
            item.SetActive(false);
        }
        //Debug.Log("Index" + index);
        if (index > 0) items[index - 1].SetActive(true);
    }
}
