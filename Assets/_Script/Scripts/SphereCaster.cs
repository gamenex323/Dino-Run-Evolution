using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SphereCaster : MonoBehaviour
{
    public bool IsHitting = false;
    public RaycastHit hit;
    [SerializeField] private float RayLength, radius;
    [SerializeField] private LayerMask layermask;

    Vector3 Hitpoint;

    //public void Update()
    //{
    //    CastRay();
    //}
    public void CastRay()
    {
        if (Physics.SphereCast(transform.position, radius, transform.forward, out hit, RayLength, layermask))
        {
            //Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green, 1);

            Hitpoint = hit.point;
            IsHitting = true;
        }
        else
        {
            IsHitting = false;
            //Debug.DrawRay(transform.position, transform.forward * RayLength, Color.red, 1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        //if(hit.collider)
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * RayLength);
        Gizmos.DrawWireSphere(transform.position + transform.forward * RayLength, radius);
    }
}
