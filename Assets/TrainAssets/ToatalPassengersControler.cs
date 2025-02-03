using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ToatalPassengersControler : MonoBehaviour
{
    public List<GameObject> PassengersList = new List<GameObject>();
    public int CounterPassengers;
    // Start is called before the first frame update
    void Start()
    {
        Triggers.instance.SecondControl = false;

    }
    private void Awake()
    {
    }
    public void PassengersStop()
    {


        for (int i = 0; i < PassengersList.Count; i++)
        {
            //print("List");
            PassengersList[i].GetComponent<NavMeshAgent>().speed = 0;
            //  yield return StartCoroutine(Lerp6());
        }

    }
    public void Destroy()
    {
        for (int i = 0; i < PassengersList.Count; i++)
        {
            // print("List");
            PassengersList[i].GetComponent<player>().Wait();
            //  yield return StartCoroutine(Lerp6());
        }

    }
    public void PassengersMove()
    {
        print("fff");

        for (int i = 0; i < PassengersList.Count; i++)
        {
            // print("List");
            PassengersList[i].GetComponent<NavMeshAgent>().speed = 20;
            //  yield return StartCoroutine(Lerp6());
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Triggers.instance.SecondControl == true)
        {
            if (Triggers.instance.FinalDes == false)
            {
                //g  GameManager.instance.Activatetriggers = true;
                //  if (GameManager.Instance.b == 1)
                // {
                //g  GameManager.instance.AddCounterForTutorial++;
                // PassengersMove();

                //g GameManager.instance.HoldUIDeActive();
                //g  if (GameManager.instance.AddCounterForTutorial >= 40)
                //{
                //    //g  if (GameManager.instance) GameManager.instance.DeactivateTutoraiLoad = true;
                //    if (Triggers.instance.SecondControl == true)
                //    {
                //        //g GameManager.instance.LeaveUIActive();
                //    }
                //}
                //  }
                //  else
                // {
                //     PassengersMove();


                // }

            }
        }
    }
}
// }
// else if (Input.GetMouseButtonUp(0) && SnakeMovement.Instance.IsFailOnDes == false)
//  {
//    if (Triggers.instance.FinalDes == false)
// {
//g  GameManager.instance.Activatetriggers = false;

//   Triggers.instance.SecondControl = false;
//   if (GameManager.Instance.b == 1)
//  {
//g if (GameManager.instance?.DeactivateTutoraiLoad == true)
//  {
////  PassengersStop();
//  Triggers.instance.StartPlayer();
//g   GameManager.instance.LeaveUIDeActive();

// }
//g if (GameManager.instance?.DeactivateTutoraiLoad == false && Triggers.instance.SecondControl == true)
//  {
//  PassengersStop();

//g GameManager.instance.HoldUIActive();

// }
// }
//  else
//   {
//     PassengersStop();
//Triggers.instance.StartPlayer();

// }
//   }
//    }

//  }
//  }
