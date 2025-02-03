using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundryDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SquidMan"))
        {
            other.transform.position = other.gameObject.GetComponentInChildren<PlayerDetection>().initialPosition;
        }
    }
}
