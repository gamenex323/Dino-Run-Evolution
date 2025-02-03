using System.Collections;
using System.Collections.Generic;
using THEBADDEST.CharacterController3;
using THEBADDEST.InteractSyetem;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (this.gameObject.GetComponentInParent<Collectable>().levelCount <= ReferenceManager.instance.characterController.currentLevelCount)
                {
                    if (this != null)
                    {
                        this.gameObject.GetComponentInParent<MoveTowardPlayer>().speed = 6.5f;
                    }

                    this.gameObject.GetComponent<Animator>().SetTrigger("Walk");
                    GetComponentInParent<MoveTowardPlayer>().isOppositeDirection = true;
                    GetComponentInParent<MoveTowardPlayer>().isReached = false;
                }
                else
                {
                    this.gameObject.GetComponent<Animator>().SetTrigger("Walk");
                    GetComponentInParent<MoveTowardPlayer>().isTriggerCheck = true;
                    GetComponentInParent<MoveTowardPlayer>().isReached = false;
                }
            }
        }
    }
}
