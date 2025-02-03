using DG.Tweening;
using GameAssets.GameSet.GameDevUtils.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using THEBADDEST.CharacterController3;
using UnityEngine;

namespace THEBADDEST.CharacterController3
{
    public class PlayerSpeedBooster : MonoBehaviour
    {
        private CharacterController upgradeCharacterSpeed;
        public GameObject playerBooster;
        public HurdleObjects hurdleObjects;
        ParticleSystem particle;

        private void Start()
        {
            upgradeCharacterSpeed = FindObjectOfType<CharacterController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //_Sajid_Saingal  AdController.instance?.ShowRewarded(TriggerUpgradeCharacterSpeed);
            }
        }

        void TriggerUpgradeCharacterSpeed()
        {

            //_Sajid_Saingal  FirebaseConnection.instance?.Check_RewardedAdsOn_InsectScene_Booster();

            if (ReferenceManager.instance.uIManager.boosterTutorialPanel)
                ReferenceManager.instance.uIManager.boosterTutorialPanel.SetActive(false);

            Vibration.VibrateNope();

            for (int i = 0; i < hurdleObjects.levelhurdelObjects.Length; i++)
            {
                hurdleObjects.levelhurdelObjects[i].GetComponentInChildren<Collider>().enabled = false;
            }

            UpgardeManager.instace.speed += 5;
            upgradeCharacterSpeed.splineMove.ChangeSpeed(UpgardeManager.instace.speed);


            particle = Instantiate(upgradeCharacterSpeed.playerBoostpar, upgradeCharacterSpeed.playerBoostpar_SpawnPoint.transform);

            float DestroyBoostPar = 2.1f;
            Invoke("DestroyBoostParticle", DestroyBoostPar);

            StartCoroutine(ResetSpeed());
        }

        private IEnumerator ResetSpeed()
        {
            yield return new WaitForSeconds(2f);

            for (int i = 0; i < hurdleObjects.levelhurdelObjects.Length; i++)
            {
                hurdleObjects.levelhurdelObjects[i].GetComponentInChildren<Collider>().enabled = true;
            }

            UpgardeManager.instace.speed -= 5;
            upgradeCharacterSpeed.splineMove.ChangeSpeed(UpgardeManager.instace.speed);
        }

        void DestroyBoostParticle()
        {
            Destroy(particle.gameObject);
        }
    }

    [Serializable]
    public class HurdleObjects
    {
        public GameObject[] levelhurdelObjects;
    }
}

