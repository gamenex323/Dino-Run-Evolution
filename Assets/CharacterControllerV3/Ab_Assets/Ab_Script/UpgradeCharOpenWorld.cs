using DG.Tweening;
using GameAssets.GameSet.GameDevUtils.Managers;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;

namespace THEBADDEST.CharacterController3
{
    public class UpgradeCharOpenWorld : MonoBehaviour
    {
        private CharacterController upgradeCharacterOpenWorld;
        public GameObject rewardRange;
        public GameObject upgradeCharPanel;

        private void Start()
        {
            upgradeCharacterOpenWorld = FindObjectOfType<CharacterController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other != null)
            {
                if (other.CompareTag("Player"))
                {
                    upgradeCharacterOpenWorld.panelsToDeactivate = GetComponent<UpgradeCharOpenWorld>();

                    DOVirtual.DelayedCall(0.5f, () => upgradeCharacterOpenWorld.insectController.insectJoystick.gameObject.SetActive(false));
                    DOVirtual.DelayedCall(0.5f, () => upgradeCharacterOpenWorld.insectController.isMoving = false);
                    DOVirtual.DelayedCall(0.5f, () => upgradeCharacterOpenWorld.insectController.insectMoveSpeed = 0);

                    upgradeCharPanel.SetActive(true);
                }
            }
        }

        public void UpgradeCharacterOpenWorld()
        {

            //_Sajid_Saingal FirebaseConnection.instance?.Check_RewardedAdsOn_CharacterUpgradeOpenWorld();

            Vibration.VibrateNope();
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.upGrade, 1);

            upgradeCharPanel.SetActive(false);

            rewardRange.SetActive(false);

            upgradeCharacterOpenWorld.insectController.insectMoveSpeed = 8;
            upgradeCharacterOpenWorld.insectController.insectJoystick.gameObject.SetActive(true);

            upgradeCharacterOpenWorld.currentLevelCount++;
            upgradeCharacterOpenWorld.upGradelevelParticle.Play();
            upgradeCharacterOpenWorld.currentLevel = (LevelType)upgradeCharacterOpenWorld.currentLevelCount;
            upgradeCharacterOpenWorld.UpgardeLevelOfCharacter(upgradeCharacterOpenWorld.currentLevel);

            for (int i = 0; i < upgradeCharacterOpenWorld.insects.Count; i++)
            {
                upgradeCharacterOpenWorld.canvasPlayer.gameObject.transform.DOMoveY(upgradeCharacterOpenWorld.insects[i].canvusOffset + 2f, 0.01f);
            }

            upgradeCharacterOpenWorld.currentInsect.gameObject.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 0.01f);

            MoveTowardPlayer[] moveTowardPlayers = upgradeCharacterOpenWorld.insect.GetComponentsInChildren<MoveTowardPlayer>();

            for (int i = 0; i < moveTowardPlayers.Length; i++)
            {
                moveTowardPlayers[i].target = upgradeCharacterOpenWorld.currentInsect.transform;
            }

            upgradeCharacterOpenWorld.currentInsect.gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
            upgradeCharacterOpenWorld.currentInsect.gameObject.transform.GetChild(1).GetChild(0).gameObject.transform.localScale = Vector3.zero;

            upgradeCharacterOpenWorld.PlayerDetectionArea();

            if (upgradeCharacterOpenWorld.barSprite) upgradeCharacterOpenWorld.barSprite.DORestartById("Rotate");
        }
    }

}
