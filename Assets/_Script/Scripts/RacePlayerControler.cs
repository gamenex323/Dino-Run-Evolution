using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class RacePlayerControler : MonoBehaviour
{
    public static RacePlayerControler instance;
    public SnakeMovement MainSnake1, MainStake2;

    [SerializeField] float m_MovingTurnSpeed = 360;
    [SerializeField] float m_StationaryTurnSpeed = 180;
    [SerializeField] public float m_JumpPower = 12f;
    [Range(0.1f, 4f)][SerializeField] float m_GravityMultiplier = 2f;
    [SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
    [SerializeField] float m_MoveSpeedMultiplier = 1f;
    [SerializeField] AnimationCurve m_AccelerationCurve;
    [SerializeField] float m_AnimSpeedMultiplier = 1f;
    [SerializeField] float m_GroundCheckDistance = 0.1f;
    Rigidbody m_Rigidbody;
    public Animator m_Animator;
    public bool m_IsGrounded;
    float m_OrigGroundCheckDistance;
    const float k_Half = 0.5f;
    float m_TurnAmount;
    float m_ForwardAmount;
    Vector3 m_GroundNormal;
    float m_CapsuleHeight;
    Vector3 m_CapsuleCenter;
    public CapsuleCollider m_Capsule;
    bool m_Crouching;


    public Transform Child;
    public float ChildHSpeed;
    public float ChildFSpeed;

    private bool OneTime;

    public bool WalkOnRode, IsTriggerOnSkeleton, IsTriggerHurdle;
    public GameObject ok;

    // rayCasting
    public bool IsHitting = false;
    public RaycastHit hit;
    [SerializeField] private float RayLength;
    [SerializeField] private LayerMask layermask;
    public Text PlayerPointText;
    Vector3 Hitpoint;
    public void CastRay()
    {
        if (Physics.Raycast(transform.position, (-transform.up), out hit, RayLength, layermask))
        {
            Hitpoint = hit.point;

            //Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green, 1);
            transform.position = Vector3.Lerp(transform.position, Hitpoint, Time.deltaTime * 10);
            IsHitting = true;
        }
        else
        {
            IsHitting = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (-transform.up) * RayLength);
    }
    private void Awake()
    {
        instance = this;

        a = 30;
        b = 8;
    }

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_CapsuleHeight = m_Capsule.height;
        m_CapsuleCenter = m_Capsule.center;
        m_OrigGroundCheckDistance = m_GroundCheckDistance;
    }

    public void Move(Vector3 move, bool crouch, bool jump)
    {
        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);
        CheckGroundStatus();
        move = Vector3.ProjectOnPlane(move, m_GroundNormal);
        m_TurnAmount = Mathf.Atan2(move.x, move.z);
        m_ForwardAmount = move.z;

        if (m_IsGrounded)
        {
            if (OneTime)
            {

                OneTime = false;

            }

            m_GravityMultiplier = 2;
            HandleAirborneMovement();
            Acce += Time.deltaTime / 6;
            ClampAcceleration();


            m_Rigidbody.isKinematic = true;
            transform.position += transform.forward * Time.deltaTime * ChildFSpeed * m_ForwardAmount;
            //m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_Rigidbody.velocity.y, m_ForwardAmount * ChildFSpeed * Acce * Time.deltaTime);
            m_Animator.Play("Run");
            //m_Animator.SetBool("Fly", false);
        }
    }

    public void PlayIdleAnim()
    {
        m_Animator.Play("Idle");
    }

    public void MoveTowardsCenter()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");

        Vector3 IniRot = Child.transform.localEulerAngles;
        Quaternion Rot = Quaternion.Euler(IniRot);
        Quaternion NextRot = Quaternion.Euler(IniRot);
        if (WalkOnRode == false)
        {
            Vector3 Inipos = Child.transform.localPosition;
            Vector3 LastPos = new Vector3(0, Inipos.y, Inipos.z);


            Child.transform.localPosition = Vector3.Lerp(Child.transform.localPosition, LastPos, Time.deltaTime * Mathf.Abs(ChildHSpeed / 15));
            //Child.transform.localPosition = Vector3.Lerp(Child.transform.localPosition, LastPos, Time.deltaTime * Mathf.Abs(ChildHSpeed / 15));
            if (h < 0)
            {
                //Debug.Log("A");
                Quaternion Rot_R = Quaternion.Euler(new Vector3(0, 0, 0));
                NextRot = Quaternion.Lerp(NextRot, Rot_R, Time.deltaTime * Mathf.Abs(h * ChildHSpeed));
            }
            else if (h > 0)
            {
                //Debug.Log("B");

                Quaternion Rot_L = Quaternion.Euler(new Vector3(0, 0, 0));
                NextRot = Quaternion.Lerp(NextRot, Rot_L, Time.deltaTime * Mathf.Abs(h * ChildHSpeed));
            }
            else
            {
                //Debug.Log("C");

                Quaternion Rot_Idle = Quaternion.Euler(new Vector3(0f, 0, 0f));
                NextRot = Quaternion.Lerp(NextRot, Rot_Idle, Time.deltaTime * Mathf.Abs(ChildHSpeed / 10));
            }
        }
        else if (WalkOnRode == true)
        {
            if (h < 0)
            {
                //Debug.Log("A");
                Quaternion Rot_R = Quaternion.Euler(new Vector3(0, 0, 0));
                NextRot = Quaternion.Lerp(NextRot, Rot_R, Time.deltaTime * Mathf.Abs(h * ChildHSpeed));
            }
            else if (h > 0)
            {
                //Debug.Log("B");

                Quaternion Rot_L = Quaternion.Euler(new Vector3(0, 0, 0));
                NextRot = Quaternion.Lerp(NextRot, Rot_L, Time.deltaTime * Mathf.Abs(h * ChildHSpeed));
            }
            else
            {
                //Debug.Log("C");

                Quaternion Rot_Idle = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                NextRot = Quaternion.Lerp(NextRot, Rot_Idle, Time.deltaTime * Mathf.Abs(ChildHSpeed / 10));
            }
        }
        Child.transform.localRotation = NextRot;
    }

    public void ChildMovement()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        //Debug.Log("H=" + h);


        Vector3 IniRot = Child.transform.localEulerAngles;
        Quaternion NextRot = Quaternion.Euler(IniRot);
        if (WalkOnRode == false)
        {
            Child.transform.localPosition = new Vector3(Child.transform.localPosition.x + h * 220 * Time.deltaTime,
                                                 Child.transform.localPosition.y, Child.transform.localPosition.z);
            //Vector3 pos = Child.transform.localPosition - transform.right * ChildHSpeed * h * Time.deltaTime;
            Vector3 pos = Child.transform.localPosition;

            //pos.x = Mathf.Clamp(pos.x, -1.2f, 1.2f);
            pos.x = Mathf.Clamp(pos.x, -6f, 6f);

            Child.transform.localPosition = pos;
            if (h < 0)
            {
                //Debug.Log("A");
                Quaternion Rot_R = Quaternion.Euler(new Vector3(0, 0, 0));
                NextRot = Quaternion.Lerp(NextRot, Rot_R, Time.deltaTime * Mathf.Abs(h * ChildHSpeed));
            }
            else if (h > 0)
            {
                //Debug.Log("B");

                Quaternion Rot_L = Quaternion.Euler(new Vector3(0, 0, 0));
                NextRot = Quaternion.Lerp(NextRot, Rot_L, Time.deltaTime * Mathf.Abs(h * ChildHSpeed));
            }
            else
            {
                //Debug.Log("C");

                Quaternion Rot_Idle = Quaternion.Euler(new Vector3(0f, 0, 0f));
                NextRot = Quaternion.Lerp(NextRot, Rot_Idle, Time.deltaTime * Mathf.Abs(ChildHSpeed / 10));
            }
        }
        else if (WalkOnRode == true)
        {
            if (h < 0)
            {
                //Debug.Log("A");
                Quaternion Rot_R = Quaternion.Euler(new Vector3(0, 0, 0));
                NextRot = Quaternion.Lerp(NextRot, Rot_R, Time.deltaTime * Mathf.Abs(h * ChildHSpeed));
            }
            else if (h > 0)
            {
                //Debug.Log("B");

                Quaternion Rot_L = Quaternion.Euler(new Vector3(0, 0, 0));
                NextRot = Quaternion.Lerp(NextRot, Rot_L, Time.deltaTime * Mathf.Abs(h * ChildHSpeed));
            }
            else
            {
                //Debug.Log("C");

                Quaternion Rot_Idle = Quaternion.Euler(new Vector3(0f, 0, 0f));
                NextRot = Quaternion.Lerp(NextRot, Rot_Idle, Time.deltaTime * Mathf.Abs(ChildHSpeed / 10));
            }
        }
        Child.transform.localRotation = NextRot;
    }

    void ScaleCapsuleForCrouching(bool crouch)
    {
        if (m_IsGrounded && crouch)
        {
            if (m_Crouching) return;
            m_Capsule.height = m_Capsule.height / 2f;
            m_Capsule.center = m_Capsule.center / 2f;
            m_Crouching = true;
        }
        else
        {
            Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
            float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
            if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                m_Crouching = true;
                return;
            }
            m_Capsule.height = m_CapsuleHeight;
            m_Capsule.center = m_CapsuleCenter;
            m_Crouching = false;
        }
    }

    void PreventStandingInLowHeadroom()
    {
        // prevent standing up in crouch-only zones
        if (!m_Crouching)
        {
            Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
            float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
            if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                m_Crouching = true;
            }
        }
    }

    public void jumpWait()
    {
        m_Animator.GetComponent<Animator>().SetBool("Jump", false);
    }

    public void Speed(GameObject Current)
    {
        Current.transform.position += Current.transform.forward * Time.deltaTime * ChildFSpeed * m_ForwardAmount;
    }

    public int a, b;

    private void FixedUpdate()
    {
        //if (RaceLevelsManager.Instance.IsWayPoint == false)
        //{
        //    transform.Translate(Vector3.forward * ChildFSpeed * Time.fixedDeltaTime);
        //}
    }

    public void Jump(int JumpvAlue)
    {
        print("thatsit");
        m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, JumpvAlue, m_Rigidbody.velocity.z);

    }

    void UpdateAnimator(Vector3 move)
    {
        return;
        // update the animator parameters
        m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
        m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
        m_Animator.SetBool("Crouch", m_Crouching);
        m_Animator.SetBool("OnGround", m_IsGrounded);
        if (!m_IsGrounded)
        {
            m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
        }

        // calculate which leg is behind, so as to leave that leg trailing in the jump animation
        // (This code is reliant on the specific run cycle offset in our animations,
        // and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
        float runCycle =
            Mathf.Repeat(
                m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
        float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;
        if (m_IsGrounded)
        {
            m_Animator.SetFloat("JumpLeg", jumpLeg);
        }

        // the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
        // which affects the movement speed because of the root motion.
        if (m_IsGrounded && move.magnitude > 0)
        {
            m_Animator.speed = m_AnimSpeedMultiplier;
        }
        else
        {
            // don't use that while airborne
            m_Animator.speed = 1;
        }
    }

    void HandleAirborneMovement()
    {
        Acce += Time.deltaTime;
        ClampAcceleration();
        Vector3 v = (transform.forward * Acce) / Time.deltaTime;
        Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
        //extraGravityForce += v;
        m_Rigidbody.AddForce(extraGravityForce);

        m_GroundCheckDistance = m_Rigidbody.velocity.y <= 0 ? m_OrigGroundCheckDistance : 0.01f;
    }

    void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
        transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
    }

    float Acce = 0;

    void ClampAcceleration()
    {
        Acce = Mathf.Clamp(Acce, 0, 1);
    }

    float waitTime = 0.1f;
    float CurrentTime;
    public LayerMask LM;
    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance, LM))
        {
            m_GroundNormal = hitInfo.normal;
            m_IsGrounded = true;
        }
        else
        {
            m_IsGrounded = false;
        }
    }
}
