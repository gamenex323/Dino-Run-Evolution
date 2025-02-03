using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookAtCamera : MonoBehaviour
{
    void Update()
    {
        // Face the text towards the camera
        transform.LookAt(Camera.main.transform);
    }
}
