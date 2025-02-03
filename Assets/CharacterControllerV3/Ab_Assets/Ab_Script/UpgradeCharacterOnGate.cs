using DG.Tweening;
using GameAssets.GameSet.GameDevUtils.Managers;
using MergeSystem;
using System.Collections;
using System.Collections.Generic;
using THEBADDEST.CharacterController3;
using THEBADDEST.InteractSyetem;
using UnityEngine;

namespace THEBADDEST.CharacterController3
{
    public class UpgradeCharacterOnGate : TriggerEffector
    {
        private CharacterController upgradeCharacterOnGate;
        public GameObject gate_Panel;

        private void Start()
        {
            upgradeCharacterOnGate = FindObjectOfType<CharacterController>();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //_Sajid_Saingal  AdController.instance?.ShowRewarded(TriggerUpgradeCharacterOnGate);
            }
        }

        void TriggerUpgradeCharacterOnGate()
        {

            //_Sajid_Saingal  FirebaseConnection.instance?.Check_RewardedAdsOn_CharacterUpgradeOnGate();

            if (ReferenceManager.instance.uIManager.cageTutorialPanel)
                ReferenceManager.instance.uIManager.cageTutorialPanel.SetActive(false);

            gate_Panel.SetActive(false);
            Vibration.VibrateNope();
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.upGrade, 1);

            upgradeCharacterOnGate.currentLevelCount++;
            upgradeCharacterOnGate.upGradelevelParticle.Play();
            upgradeCharacterOnGate.currentLevel = (LevelType)upgradeCharacterOnGate.currentLevelCount;
            upgradeCharacterOnGate.UpgardeLevelOfCharacter(upgradeCharacterOnGate.currentLevel);

            if (upgradeCharacterOnGate.barSprite) upgradeCharacterOnGate.barSprite.DORestartById("Rotate");
        }
    }
}

