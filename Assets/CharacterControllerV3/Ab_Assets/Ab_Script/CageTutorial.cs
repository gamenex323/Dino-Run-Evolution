using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageTutorial : MonoBehaviour
{
    public Transform cageTarget;
    public GameObject obj;
    public bool iscage = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UpgardeManager.instace.speed = 0;
            ReferenceManager.instance.characterController.splineMove.ChangeSpeed(UpgardeManager.instace.speed);

            if (ReferenceManager.instance.uIManager.levelNo == 1)
            {
                ReferenceManager.instance.uIManager.hurdleTutorialPanel.SetActive(true);
            }
            else if (ReferenceManager.instance.uIManager.levelNo == 2)
            {
                ReferenceManager.instance.uIManager.boosterTutorialPanel.SetActive(true);
            }
            else
            {
                ReferenceManager.instance.uIManager.cageTutorialPanel.SetActive(true);
            }

            obj.SetActive(true);
            iscage = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (iscage)
        {
            if (Input.GetMouseButtonDown(0))
            {
                UpgardeManager.instace.speed = 5;
                ReferenceManager.instance.characterController.splineMove.ChangeSpeed(UpgardeManager.instace.speed);
            }
            if (Input.GetMouseButtonUp(0))
            {
                UpgardeManager.instace.speed = 0;
                ReferenceManager.instance.characterController.splineMove.ChangeSpeed(UpgardeManager.instace.speed);
            }

            Vector3 screenPoint = Camera.main.WorldToScreenPoint(cageTarget.position);

            if (ReferenceManager.instance.uIManager.levelNo == 1)
            {
                ReferenceManager.instance.uIManager.hurdleCircle.position = screenPoint;
            }
            else if (ReferenceManager.instance.uIManager.levelNo == 2)
            {
                ReferenceManager.instance.uIManager.boosterCircle.position = screenPoint;
            }
            else
            {
                ReferenceManager.instance.uIManager.cageCircle.position = screenPoint;
            }
        }
    }
}
