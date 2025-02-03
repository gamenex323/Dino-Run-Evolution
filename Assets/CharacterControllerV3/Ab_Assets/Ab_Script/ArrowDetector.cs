using UnityEngine.UI;
using UnityEngine;
using THEBADDEST.InteractSyetem;

public class ArrowDetector : MonoBehaviour
{
    public static ArrowDetector instance;
    public Transform target; // Reference to the specific target

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // Look at the specific target
            transform.LookAt(target);
        }
    }
}
