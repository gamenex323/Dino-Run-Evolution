using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using THEBADDEST.InteractSyetem;
using GameAssets.GameSet.GameDevUtils.Managers;
using THEBADDEST.CharacterController3;
using DG.Tweening;

namespace InsectEvolution
{
    public class Hurdle : TriggerEffector
    {
        THEBADDEST.CharacterController3.CharacterController characterController;
        bool isDestroy;

        protected override void OnEffect(Collider other, IEffectContainer container)
        {
            base.OnEffect(other, container);
            if (other.CompareTag("Player"))
            {
                characterController = other.GetComponent<THEBADDEST.CharacterController3.CharacterController>();

                if (!isDestroy)
                {
                    GameController.changeGameState(GameState.Fail);
                    characterController.virtualCamera.enabled = false;
                    characterController.transform.DOShakeRotation(0.2f, new Vector3(300f, 300f, 300f)).SetEase(Ease.Linear).OnComplete(delegate
                    {
                        characterController.transform.DORotate(new Vector3(0, 180f, 0), 0.35f).OnComplete(delegate
                             {
                                 characterController.transform.DOScale(Vector3.zero, 0.4f).OnComplete(delegate
                                 {
                                     characterController.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.001f);
                                     Vibration.VibratePeek();
                                     SoundManager.Instance.PlayOneShot(SoundManager.Instance.bombHitClip, 1);
                                     Instantiate(characterController.splashPrefeb, characterController.transform.position, characterController.splashPrefeb.transform.rotation);

                                     characterController.currentInsect.SetActive(false);
                                 });
                             });
                    });

                    isDestroy = true;
                }

                characterController.GetComponent<Rigidbody>().isKinematic = true;
                characterController.GetComponent<Collider>().enabled = false;
            }
        }
    }
}