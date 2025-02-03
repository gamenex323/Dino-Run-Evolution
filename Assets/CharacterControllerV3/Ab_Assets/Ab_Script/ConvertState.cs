using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertState : MonoBehaviour
{
    THEBADDEST.CharacterController3.CharacterController characterController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("CameraChange_000");
            characterController = other.GetComponent<THEBADDEST.CharacterController3.CharacterController>();
            characterController.currentInsectAnimator.SetTrigger("Idle");
        }
    }
}
