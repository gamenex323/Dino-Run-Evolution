using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCageTutorial : MonoBehaviour
{
    public GameObject closeCageTrigger;
    public GameObject openCageTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.CompareTag("Player"))
            {
                openCageTrigger.SetActive(false);
                closeCageTrigger.SetActive(false);

                if (ReferenceManager.instance.uIManager.levelNo == 1)
                {
                    ReferenceManager.instance.uIManager.hurdleTutorialPanel.SetActive(false);
                }
                else if (ReferenceManager.instance.uIManager.levelNo == 2)
                {
                    ReferenceManager.instance.uIManager.boosterTutorialPanel.SetActive(false);
                }
                else
                {
                    ReferenceManager.instance.uIManager.cageTutorialPanel.SetActive(false);
                }
            }
        }
    }
}
