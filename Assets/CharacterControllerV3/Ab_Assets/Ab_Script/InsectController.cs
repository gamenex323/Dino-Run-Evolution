using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityStandardAssets.CrossPlatformInput;

public class InsectController : MonoBehaviour
{
    [SerializeField] public Rigidbody insectRigidbody;
    public FloatingJoystick insectJoystick;
    public float insectMoveSpeed;
    [SerializeField] public float rotationSpeed;
    [SerializeField] public float deadZone;
    public bool isMoving = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            ReferenceManager.instance.uIManager.homeOpenWorldPanel.SetActive(false);
        }

        Vector3 joystickInput = new Vector3(insectJoystick.Horizontal, 0, insectJoystick.Vertical);

        // Check if joystick movement exceeds the dead zone
        if (joystickInput.magnitude > deadZone)
        {
            // If moving, set trigger and update velocity
            if (!isMoving)
            {
                isMoving = true;
                ReferenceManager.instance.characterController.currentInsectAnimator.SetTrigger("Walk");
            }

            // Use the forward direction of the insect for movement
            Vector3 insectMovementDirection = transform.forward.normalized;

            insectRigidbody.velocity = new Vector3(insectMovementDirection.x * insectMoveSpeed, insectRigidbody.velocity.y,
                insectMovementDirection.z * insectMoveSpeed);

            // Rotate towards the joystick input direction
            Quaternion targetRotation = Quaternion.LookRotation(joystickInput.normalized, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // If not moving, set trigger and reset velocity
            if (isMoving)
            {
                isMoving = false;
                ReferenceManager.instance.characterController.currentInsectAnimator.SetTrigger("Idle");
            }

            insectRigidbody.velocity = Vector3.zero;
        }
    }
}
