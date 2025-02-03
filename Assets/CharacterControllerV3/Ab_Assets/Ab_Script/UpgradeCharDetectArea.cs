using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using THEBADDEST.CharacterController3;
using Unity.VisualScripting;
using UnityEngine;

namespace THEBADDEST.CharacterController3
{
    public class UpgradeCharDetectArea : MonoBehaviour
    {
        private CharacterController characterController;
        public GameObject upgradeCharDetectAreaPanel;

        private void Start()
        {
            characterController = FindObjectOfType<CharacterController>();
            upgradeCharDetectAreaPanel.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                DOVirtual.DelayedCall(0.5f, () => characterController.insectController.insectJoystick.gameObject.SetActive(false));
                DOVirtual.DelayedCall(0.5f, () => characterController.insectController.isMoving = false);
                DOVirtual.DelayedCall(0.5f, () => characterController.insectController.insectMoveSpeed = 0);

                upgradeCharDetectAreaPanel.SetActive(true);
            }
        }

        public void OnClickNoUpgradeCharacterAreaButton()
        {
            upgradeCharDetectAreaPanel.SetActive(false);
            characterController.insectController.insectMoveSpeed = 8;
            characterController.insectController.insectJoystick.gameObject.SetActive(true);
        }

        public void OnClickYesUpgradeCharacterAreaButton()
        {
            //_Sajid_Saingal
            //////if (AdController.instance)
            //////{
            //////    AdController.instance.ShowRewarded(UpgradePlayerDetectionArea);
            //////}
            //////else
            //////{
            upgradeCharDetectAreaPanel.SetActive(false);
            characterController.insectController.insectMoveSpeed = 8;
            characterController.insectController.insectJoystick.gameObject.SetActive(true);
            ////  }
        }

        public void UpgradePlayerDetectionArea()
        {
            upgradeCharDetectAreaPanel.SetActive(false);
            characterController.insectController.insectMoveSpeed = 8;
            characterController.insectController.insectJoystick.gameObject.SetActive(true);

            characterController.detectAreaParticleSystem.gameObject.transform.localScale += Vector3.one * 0.05f;
            characterController.LookAtManager.GetColliders(characterController.detectAreaParticleSystem.gameObject.transform.localScale);
        }
    }
}
