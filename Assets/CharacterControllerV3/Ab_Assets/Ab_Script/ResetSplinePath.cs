using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSplinePath : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ReferenceManager.instance.characterController.StartOnNormalPathClampBehaviours();
            ReferenceManager.instance.levelManager.useRewardedPath = false;
        }
    }
}
