using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using System;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControler : MonoBehaviour
{
    public float clampValueMax;
    public float clampValueMin;
    //Rigidbody m_Rigidbody;
    public Transform Child;
    public float ChildHSpeed;
    public bool ISCrossPlateFormInput;

    void Start()
    {
        //  m_Rigidbody = GetComponent<Rigidbody>();
        //  m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ |
        //  RigidbodyConstraints.FreezePositionX;
    }

    public void ChildMovement_InputCrossPlateForm()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        //	float h = Inp;

        Child.transform.localPosition = new Vector3(Child.transform.localPosition.x + h * 100 * Time.deltaTime,
                                                 Child.transform.localPosition.y, Child.transform.localPosition.z);

        Vector3 pos = Child.transform.localPosition;
        pos.x = Mathf.Clamp(pos.x, clampValueMin, clampValueMax);
        Child.transform.localPosition = pos;
    }

    public void ChildMovement_InputMouse()
    {
        if (Input.GetMouseButton(0))
        {
            float h = CrossPlatformInputManager.GetAxis("Horizontal");

            Child.transform.localPosition = new Vector3(Child.transform.localPosition.x + h * 100 * Time.deltaTime,
                                                 Child.transform.localPosition.y, Child.transform.localPosition.z);

            Vector3 pos = Child.transform.localPosition;
            pos.x = Mathf.Clamp(pos.x, clampValueMin, clampValueMax);
            Child.transform.localPosition = pos;
        }

    }

    private void FixedUpdate()
    {
        if (ISCrossPlateFormInput)
        {
            ChildMovement_InputCrossPlateForm();
        }
        if (ISCrossPlateFormInput == false)
        {
            ChildMovement_InputMouse();
        }
    }
}

