using System.Collections;
using System.Collections.Generic;
using THEBADDEST.InteractSyetem;
using UnityEngine;

public class BulletTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.transform.GetComponent<Skull>())
        //{
        //    other.gameObject.SetActive(false);
        //}

        Debug.Log("Trigger With " + other.name);
    }
}
