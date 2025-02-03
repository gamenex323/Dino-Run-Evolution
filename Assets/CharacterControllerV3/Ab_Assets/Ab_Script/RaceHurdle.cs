using DG.Tweening;
using GameAssets.GameSet.GameDevUtils.Managers;
using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using THEBADDEST.CharacterController3;
using THEBADDEST.InteractSyetem;
using Unity.VisualScripting;
using UnityEngine;

public class RaceHurdle : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RacePlayer"))
        {
            if (other.gameObject.GetComponentInParent<RacePlayer>())
            {
                other.gameObject.GetComponent<Collider>().enabled = false;
                other.gameObject.GetComponentInParent<PathFollower1>().enabled = false;

                var insectTransform = RaceInsect.Instance.raceInsects[RaceInsect.Instance.raceInsectCount - 1].transform;
                insectTransform.DORotate(new Vector3(0, 0, 0), 0.0001f);

                insectTransform.DOShakeRotation(0.2f, new Vector3(300f, 300f, 300f)).SetEase(Ease.Linear).OnComplete(delegate
                {
                    insectTransform.DORotate(new Vector3(0, 180f, 0), 0.35f).OnComplete(delegate
                    {
                        insectTransform.DOScale(Vector3.zero, 0.4f).OnComplete(delegate
                        {
                            insectTransform.DOLocalRotate(new Vector3(0, 0, 0), 0.001f);
                            Vibration.VibratePeek();
                            SoundManager.Instance.PlayOneShot(SoundManager.Instance.bombHitClip, 1);

                            Instantiate(RaceInsect.Instance.raceSplashPrefeb, insectTransform.position, insectTransform.rotation);

                            insectTransform.gameObject.SetActive(false);
                            RaceGameController.changeGameStateRace(GameStateRace.FailRace);
                        });
                    });
                });
            }
        }
    }
}

