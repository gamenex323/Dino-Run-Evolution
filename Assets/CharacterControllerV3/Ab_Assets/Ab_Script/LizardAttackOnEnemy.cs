using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardAttackOnEnemy : MonoBehaviour
{
    THEBADDEST.CharacterController3.CharacterController characterController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("CameraChange");
            characterController = other.GetComponent<THEBADDEST.CharacterController3.CharacterController>();
            characterController.behaviours[0].clampValueMax = 0;
            characterController.behaviours[0].clampValueMin = 0;
            characterController.cinemachine.SetTrigger("End");
        }
    }
}
