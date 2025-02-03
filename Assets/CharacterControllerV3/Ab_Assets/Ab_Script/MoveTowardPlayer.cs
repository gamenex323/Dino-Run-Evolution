using DG.Tweening;
using GameAssets.GameSet.GameDevUtils.Managers;
using System.Collections;
using System.Collections.Generic;
using THEBADDEST.CharacterController3;
using THEBADDEST.InteractSyetem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MoveTowardPlayer : MonoBehaviour
{
    public THEBADDEST.CharacterController3.CharacterController characterController;
    public static MoveTowardPlayer Instance;

    public Transform target;
    public Transform targetParent;

    public float speed = 5f;
    public bool isReached = false;
    public bool isTriggerCheck = false;
    public bool isOppositeDirection = false;
    public bool isDestination = false;
    NavMeshAgent agent;
    public Transform[] aiDestination;
    public int aiIndex;

    private void Awake()
    {
        Instance = this;
        agent = this.gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggerCheck)
        {
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);

            if (target != null && isReached == false && target.gameObject.activeInHierarchy)
            {
                transform.LookAt(target);

                Vector3 direction = isReached ? transform.position - target.position : target.position - transform.position;

                direction.Normalize();

                transform.Translate(direction * speed * Time.deltaTime, Space.World);
            }
        }

        if (isOppositeDirection)
        {
            Destination();
            isOppositeDirection = false;
            isDestination = true;
        }
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && isDestination)
        {
            aiIndex++;

            if (aiIndex >= 5)
            {
                aiIndex = 0;
            }

            Destination();
        }
    }

    void Destination()
    {
        agent.SetDestination(aiDestination[aiIndex].position);
    }
}
