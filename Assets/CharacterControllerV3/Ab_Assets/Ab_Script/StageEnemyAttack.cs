using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEnemyAttack : MonoBehaviour
{
    public GameObject stageEnemy;
    public Animator enemyAnimator;
    public float movingSpeed;
    public Tween moveEnemy;

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

            // Move only if 'other' is not null
            stageEnemy.gameObject.transform.DOMove(other.transform.position, movingSpeed, false);

            enemyAnimator.SetTrigger("Walk");
            enemyAnimator.SetBool("Idle", false);
        }
    }
}
