using System;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;

//namespace UnityStandardAssets.Characters.ThirdPerson
//{
[RequireComponent(typeof(PlayerControler))]
public class ThirdPersonUserControl : MonoBehaviour
{
    private RacePlayerControler m_Character; // A reference to the ThirdPersonCharacter on the object
    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

    public float V_Input = 1;

    public static bool MoveToCenter = false;

    public bool getPowerFromDial = false;
    public float BoostTime = 5;
    public float CurrentBoostTime = 0;

    public RayCaster LR_raycaster;
    public Transform LR;

    public float H_Offset;
    private void Start()
    {
        IsControlerActive = true;
        MoveToCenter = false;
        // get the transform of the main camera
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }

        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<RacePlayerControler>();
    }
    public bool IsControlerActive;
    private void Update()
    {
        //if (!ReferenceManager.playeingGame)
        //{
        //    m_Character.PlayIdleAnim(); return;
        //}
        if (IsControlerActive)
        {
            if (MoveToCenter)
            {
                //  m_Character.MoveTowardsCenter();
            }
            else
            {
                m_Character.ChildMovement();
            }
        }

        if (LR_raycaster)
        {
            LR_raycaster.CastRay();

            if (LR_raycaster.IsHitting)
            {
                LR.position = LR_raycaster.hit.point + Vector3.up * H_Offset;
            }
        }

    }

    private void FixedUpdate()
    {

        //if (!ReferenceManager.playeingGame)
        //{
        //    m_Character.PlayIdleAnim(); return;
        //}

        // read inputs
        //float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float h = Input.GetAxis("Horizontal");
        float v = V_Input;
        bool crouch = Input.GetKey(KeyCode.C);

        // calculate move direction to pass to character
        if (m_Cam != null)
        {
            // calculate camera relative direction to move:
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = new Vector3(0, 0, v);
            //m_Move = v*m_CamForward + h*m_Cam.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            m_Move = v * Vector3.forward + h * Vector3.right;
        }
#if !MOBILE_INPUT
        // walk speed multiplier
        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

        // pass all parameters to the character control script
        m_Character.Move(m_Move, crouch, m_Jump);
        m_Jump = false;

    }


}

