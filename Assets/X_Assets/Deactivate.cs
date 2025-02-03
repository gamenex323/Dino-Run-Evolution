using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    public float Time;

    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("WaitToDeactivate", Time);
    }

    public void WaitToDeactivate()
    {
        gameObject.SetActive(false);

    }
}
