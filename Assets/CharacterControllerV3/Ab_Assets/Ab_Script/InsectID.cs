using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectID : MonoBehaviour
{
    public int insectID;

    // Start is called before the first frame update
    void Start()
    {
        insectID = GetComponentInParent<InsectIdManager>().ReturnID(this);

        bool isSelected = PlayerPrefs.GetInt("Insect_" + insectID + "_Selected", 0) == 1;

        if (isSelected)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    // Method to handle insect selection
    public void SelectInsect()
    {
        PlayerPrefs.SetInt("Insect_" + insectID + "_Selected", 1);
        PlayerPrefs.Save();

        gameObject.SetActive(false);
    }
}
