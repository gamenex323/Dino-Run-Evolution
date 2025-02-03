using UnityEngine;
using System.Collections;


namespace SpericalGravity
{
    [RequireComponent(typeof(Rigidbody))]
    public class GravityBody : MonoBehaviour
    {
        GravityAttractor planet;
        Rigidbody rigidbody;

        void OnEnable()
        {
            planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
            rigidbody = GetComponent<Rigidbody>();

            // Disable rigidbody gravity and rotation as this is simulated in GravityAttractor script
            rigidbody.useGravity = false;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }

        void FixedUpdate()
        {
            // Allow this body to be influenced by planet's gravity
            if (planet != null)
                planet.Attract(rigidbody);
        }
    }
}