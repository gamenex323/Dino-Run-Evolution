using UnityEngine;
using System;
using System.Collections;

//[RequireComponent(typeof(Animator))]
[ExecuteInEditMode]
public class IKControl : MonoBehaviour
{

    //public static IKControl Instance;
    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //    }
    //}


    public Animator animator;

    public bool ikActive = false;
    public bool IsInEditMode = false;

    public Ik_Data ik_Data;

    public Transform SpineBone = null;


    void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void Update()
    {
        if(IsInEditMode && animator)
            animator.Update(0);
        //OnAnimatorIK();
    }

    //a callback for calculating IK
   public void OnAnimatorIK()
    {
        //Debug.Log("1");
        if (animator)
        {
            //Debug.Log("2");

            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {

                // Set the right hand target position and rotation, if one has been assigned
                if (ik_Data.RH != null)
                {
                    //Debug.Log("4");

                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, (ik_Data.RH.position));
                    animator.SetIKRotation(AvatarIKGoal.RightHand, ik_Data.RH.rotation);
                }

                if (ik_Data.LH != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, (ik_Data.LH.position));
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, ik_Data.LH.rotation);
                }

                if (ik_Data.RF != null)
                {
                    //Debug.Log("4");

                    animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
                    //animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, ik_Data.RF.position);
                    //animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFeetObj.rotation);
                }

                if (ik_Data.LF != null)
                {
                    //Debug.Log("4");

                    animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
                    //animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, ik_Data.LF.position);
                    //animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFeetObj.rotation);
                }

                if (ik_Data.LookAtTraget != null)
                {

                    animator.SetBoneLocalRotation(HumanBodyBones.Neck, ik_Data.LookAtTraget.rotation);
                }

                if (ik_Data.Spine_01 != null)
                {
                    var targetRotation = Quaternion.LookRotation(ik_Data.Spine_01.position - SpineBone.position);
                    //Char_Target.localRotation = Quaternion.Lerp(Char_Target.localRotation, targetRotation, Time.deltaTime * TurnSpeed);
                    animator.SetBoneLocalRotation(HumanBodyBones.Spine, targetRotation);

                }

            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);
                animator.SetLookAtWeight(0);
            }
        }
    }
}


