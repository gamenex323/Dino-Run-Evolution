using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchGate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<THEBADDEST.CharacterController3.CharacterController>())
        {
            this.GetComponent<BoxCollider>().enabled = false;
            //_Sajid_Saingal AdController.instance?.ShowRewarded(UpGrade);
        }
    }

    void UpGrade()
    {
        int level = (int)ReferenceManager.instance.characterController.currentLevel;
        level++;
        ReferenceManager.instance.characterController.UpgardeLevelOfCharacter((LevelType)level);
    }
}
