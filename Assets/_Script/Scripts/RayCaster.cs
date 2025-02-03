using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RayCaster : MonoBehaviour
{
    public bool IsHitting = false;
    public RaycastHit hit;
    [SerializeField] private float RayLength;
    [SerializeField] private LayerMask layermask;

    Vector3 Hitpoint;

    //public void Update()
    //{
    //    CastRay();
    //}
    public void CastRay()
    {
        if (Physics.Raycast(transform.position, -transform.up, out hit, RayLength, layermask))
        {
            Hitpoint = hit.point;

            //Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green, 1);
            transform.position = Vector3.Lerp(transform.position, Hitpoint, Time.deltaTime*10);
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
        Gizmos.DrawLine(transform.position, transform.position + (-transform.up) * RayLength);
    }
}
