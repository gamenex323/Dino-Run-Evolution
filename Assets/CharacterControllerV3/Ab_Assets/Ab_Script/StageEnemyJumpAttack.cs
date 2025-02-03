using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEnemyJumpAttack : MonoBehaviour
{
    public GameObject stageEnemy;
    public Animator enemyAnimator;
    public float movingSpeed;
    public bool canMoveComplete;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator.SetBool("Idle", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject.CompareTag("Player"))
        {
            stageEnemy.transform.LookAt(other.gameObject.transform);

            stageEnemy.gameObject.transform.DOMove(other.transform.position, movingSpeed, false);

            enemyAnimator.SetTrigger("Walk");
            
            enemyAnimator.SetBool("Idle", false);
        }
    }

    void OnJump()
    {
        DOVirtual.DelayedCall(0.5f, () => enemyAnimator.SetTrigger("Jump"));
    }
}
