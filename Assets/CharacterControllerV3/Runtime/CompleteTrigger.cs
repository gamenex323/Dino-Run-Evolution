using UnityEngine;
using DG.Tweening;
using System.Collections;
using System;

namespace THEBADDEST.InteractSyetem
{
    public class CompleteTrigger : TriggerEffector
    {
        public static CompleteTrigger Instance;
        THEBADDEST.CharacterController3.CharacterController characterController;
        [SerializeField] Transform target;

        private void Awake()
        {
            Instance = this;
        }

        protected override void OnEffect(Collider other, IEffectContainer container)
        {
            //triggered = true;
            base.OnEffect(other, container);
            if (other.CompareTag("Player"))
            {
                characterController = other.GetComponent<THEBADDEST.CharacterController3.CharacterController>();
                characterController.canvasPlayer.SetActive(false);
                Destroy(characterController.m_Rigidbody);
                characterController.splineMove.ChangeSpeed(0);
                characterController.progressionbar.SetActive(false);
                characterController.behaviours[0].CanControl = false;

                characterController.currentInsectAnimator.SetTrigger("Idle");
                //BulletSpawner.Instance.StopBulletSpawning();
                //StartCoroutine(CompletePanle());
            }
        }

        public IEnumerator CompletePanle()
        {
            yield return new WaitForSeconds(.1f);
            GameController.changeGameState(GameState.Complete);
            var lvl = PlayerPrefs.GetInt("LevelOfChara" + PlayerPrefManager.GetSceneName());
            if (lvl < characterController.insects.Count)
            {
                lvl++;
                PlayerPrefs.SetInt("LevelOfChara" + PlayerPrefManager.GetSceneName(), lvl);
            }
        }
    }
}