using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;
using PathCreation.Examples;
public class FollowPlayerSpeed : MonoBehaviour
{
    public SnakeMovement OxygenCylendarMove;
    public PathFollower Path;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //if (RaceLevelsManager.Instance.IsWayPoint == false)
        //{
        //    // player.GetComponent<PlayerControler>().ChildFSpeed = 6;

        //}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.Translate(Vector3.forward * OxygenCylendarMove.speed * Time.fixedDeltaTime);
        //if (RaceLevelsManager.Instance.IsWayPoint == false)
        //{

        //    gameObject.GetComponent<FollowPlayerSpeed>().enabled = true;

        //    // transform.Translate(Vector3.forward * PlayerControler.instance.ChildFSpeed * Time.fixedDeltaTime);
        //    transform.Translate(Vector3.forward * Path.currentSpeed * Time.fixedDeltaTime);

        //}
        //if
        //{
        gameObject.GetComponent<splineMove>().enabled = true;
        player.GetComponent<splineMove>().enabled = true;
        gameObject.GetComponent<FollowPlayerSpeed>().enabled = false;
        //  }
    }
}
