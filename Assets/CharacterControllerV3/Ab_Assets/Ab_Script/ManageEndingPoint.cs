using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageEndingPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ReferenceManager.instance.levelManager.endPoint.gameObject.SetActive(true);

            if (ReferenceManager.instance.levelManager.loadlevel.transform.TryGetComponent<LevelObjectsPlacer>(out LevelObjectsPlacer levelObjectsPlacer))
            {
                if (levelObjectsPlacer.endPointAttachedTarget)
                    ReferenceManager.instance.levelManager.endPoint.position = levelObjectsPlacer.endPointAttachedTarget.position;
            }
        }
    }

}
