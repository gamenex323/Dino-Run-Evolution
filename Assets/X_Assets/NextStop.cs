using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStop : MonoBehaviour
{
    public GameObject NextObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "capsulCollider")
        {
            //g GameManager.instance.AddCounterForTutorial = 0;
            //g if (GameManager.instance) GameManager.instance.DeactivateTutoraiLoad = false;
            NextObj.SetActive(true);
        }
    }
}
