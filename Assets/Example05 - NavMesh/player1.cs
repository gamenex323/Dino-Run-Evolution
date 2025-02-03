using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class player1 : MonoBehaviour
{

    public Transform Target;
    public Vector3 Tar;
    // Use this for initialization
    void Start()
    {
        //Target = GameObject.Find("TargetOnce").transform;
        //Tar = Target.transform;
    }

    // Update is called once per frame

    public void Wait2()
    {
        gameObject.AddComponent<Rigidbody>();
        //gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;

        gameObject.GetComponent<CapsuleCollider>().isTrigger = false;

    }
    void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        agent.destination = Target.position;
        // if (GameManager.instance.Dum <= 0)
        {
            Invoke("Wait", 2);
        }
        //V3.SetDestination(Target);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "DesLast")
        {
            //g	GameManager.instance.EndCurrency++;
            gameObject.SetActive(false);

        }
    }
}
