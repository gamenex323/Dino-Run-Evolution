using UnityEngine;

public class LookToTarget : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (ReferenceManager.instance.characterController.currentInsect != null)
        {
            transform.LookAt(ReferenceManager.instance.characterController.currentInsect.transform);
        }
    }
}
