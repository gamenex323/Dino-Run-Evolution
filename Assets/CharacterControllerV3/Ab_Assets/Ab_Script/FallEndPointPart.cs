using System.Collections;
using UnityEngine;

public class FallEndPointPart : MonoBehaviour
{
    public GameObject endPointPart;
    public float delayBeforeFall = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AddGravityWithDelay();
        }
    }

    private void AddGravityWithDelay()
    {
        Rigidbody rb = endPointPart.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb.useGravity = true;
        }
    }
}
