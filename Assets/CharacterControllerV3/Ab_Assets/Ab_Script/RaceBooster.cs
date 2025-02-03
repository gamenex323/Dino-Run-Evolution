using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using THEBADDEST.CharacterController3;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace GameAssets.GameSet.GameDevUtils.Managers
{
    public class RaceBooster : MonoBehaviour
    {
        public GameObject receBoster;

        private void OnTriggerEnter(Collider other)
        {
            Vibration.VibrateNope();

            if (other.gameObject.CompareTag("RacePlayer"))
            {
                if (other.gameObject.GetComponentInParent<RacePlayer>())
                {
                    other.gameObject.GetComponentInParent<PathFollower1>().currentSpeed += 20;
                    receBoster.gameObject.SetActive(false);
                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.raceCollectBooster, 1);
                    GameManager.Instance.racePlayerBoostPar.gameObject.SetActive(true);
                    GameManager.Instance.racePlayerBoostPar.Play();

                    StartCoroutine(RaceResetSpeed(other));
                }
            }
        }

        private IEnumerator RaceResetSpeed(Collider other)
        {
            yield return new WaitForSeconds(1.65f);
            other.gameObject.GetComponentInParent<PathFollower1>().currentSpeed -= 20;
            GameManager.Instance.racePlayerBoostPar.gameObject.SetActive(false);
        }
    }
}
