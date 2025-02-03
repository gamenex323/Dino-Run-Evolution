using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using PathCreation.Examples;
using Cinemachine;
using GameAssets.GameSet.GameDevUtils.Managers;

public class PlayerUpdateText : MonoBehaviour
{
    [SerializeField] private Transform[] targetPositions;
    [SerializeField] private Transform[] endingFlag;
    private int updateSetCount = 0;
    public int updateCompleteCount = 0;
    private List<GameObject> triggeredObjects = new List<GameObject>();
    public ParticleSystem raceCompleteParticle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RacePlayer"))
        {
            Time.timeScale = 1f;

            if (other.gameObject.GetComponentInParent<RacePlayer>())
            {
                AIUpdateText.Instance.playerCamera.gameObject.SetActive(false);
                AIUpdateText.Instance.stageCamera.gameObject.SetActive(true);
            }

            if (!triggeredObjects.Contains(other.gameObject))
            {
                triggeredObjects.Add(other.gameObject);
                MoveObjectToNextPosition(other.gameObject);

                if(updateCompleteCount == 0)
                {
                    endingFlag[0].transform.DOLocalMoveY(0, 0.5f).SetEase(Ease.OutBounce);
                    DOVirtual.DelayedCall(0.55f, () => raceCompleteParticle.Play());
                }
            }
        }
    }

    private void MoveObjectToNextPosition(GameObject obj)
    {
        if (updateSetCount < targetPositions.Length)
        {
            obj.transform.DOJump(targetPositions[updateSetCount].position, 10, 1, 1f).SetEase(Ease.Linear).OnComplete(delegate
            {
                if (updateCompleteCount == 1)
                {
                    endingFlag[1].transform.DOLocalMoveY(0, 0.5f).SetEase(Ease.OutBounce);
                }
                else
                {
                    endingFlag[2].transform.DOLocalMoveY(0, 0.5f).SetEase(Ease.OutBounce);
                }

                updateCompleteCount += 1;

                if (obj.GetComponentInChildren<Animator>())
                    obj.GetComponentInChildren<Animator>().SetTrigger("Idle");

                if (obj.GetComponentInChildren<CheckText>())
                    obj.GetComponentInChildren<CheckText>().gameObject.SetActive(false);

                Quaternion targetRotation = Quaternion.Euler(0f, 180f, 0f);
                obj.transform.DORotate(targetRotation.eulerAngles, 0.25f);

                Destroy(obj.gameObject.GetComponentInParent<ThirdPersonUserControl>());
                Destroy(obj.gameObject.GetComponentInParent<PlayerControler>());
                Destroy(obj.gameObject.GetComponentInParent<RacePlayerControler>());
                Destroy(obj.gameObject.GetComponentInParent<PathFollower1>());
                Destroy(obj.gameObject.GetComponentInParent<Rigidbody>());

                if (updateCompleteCount == 3)
                {
                    DOVirtual.DelayedCall(0.25f, () =>  SoundManager.Instance.PlayOneShot(SoundManager.Instance.raceCompleteSound, 1));
                    DOVirtual.DelayedCall(1.5f, () => RaceGameController.changeGameStateRace(GameStateRace.CompleteRace));
                }
            });

            updateSetCount++;
        }
    }
}
