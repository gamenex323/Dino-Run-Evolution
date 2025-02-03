using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ToatalPassengersControler1 : MonoBehaviour
{
    public static ToatalPassengersControler1 instance;
    public List<GameObject> PassengersList = new List<GameObject>();
    public int CounterPassengers;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        SwitchBox = 1;
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
            PassengersList[i].GetComponent<player>().Wait2();
            //  yield return StartCoroutine(Lerp6());
        }

    }
    public int LoopCount, SwitchBox;
    public IEnumerator PassengersMove20()
    {


        for (int i = 0; i < 13; i++)
        {
            // print("List");
            LoopCount++;
            Invoke("Wait", 5);

            if (LoopCount >= 30)
            {
                LoopCount = 0;
                SwitchBox++;

            }

            //   GameManager.instance.TotalCountOxigen--;
            VibrationExample.instance.TapPeekVibrate();
            PassengersList[i].SetActive(true);
            PassengersList[i].GetComponent<NavMeshAgent>().speed = 20;
            SnakeMovement.Instance.tailObjects[/*SnakeMovement.Instance.tailObjects.Count -*/ SwitchBox].transform.GetChild(5).GetComponent<PassegersInSidetrain>().
              gameObject.transform.GetChild(LoopCount).gameObject.SetActive(false);
            yield return StartCoroutine(Lerp7());
        }

    }
    public IEnumerator PassengersMove45()
    {

        Invoke("Wait", 7);

        for (int i = 0; i < 70; i++)
        {
            // print("List");
            LoopCount++;

            if (LoopCount >= 12)
            {
                LoopCount = 0;
                SwitchBox++;

            }

            //   GameManager.instance.TotalCountOxigen--;
            VibrationExample.instance.TapPeekVibrate();
            PassengersList[i].SetActive(true);
            PassengersList[i].GetComponent<NavMeshAgent>().speed = 20;
            SnakeMovement.Instance.tailObjects[/*SnakeMovement.Instance.tailObjects.Count -*/ SwitchBox].transform.GetChild(5).GetComponent<PassegersInSidetrain>().
              gameObject.transform.GetChild(LoopCount).gameObject.SetActive(false);
            yield return StartCoroutine(Lerp7());
        }

    }
    public void Wait()
    {
        for (int i = 0; i < SnakeMovement.Instance.tailObjects.Count; i++)
        {
            SnakeMovement.Instance.tailObjects[i].transform.GetChild(5).gameObject.SetActive(false);

        }
    }
    public IEnumerator PassengersMove70()
    {

        Invoke("Wait", 10);
        for (int i = 0; i < 117; i++)
        {
            // print("List");
            LoopCount++;

            if (LoopCount >= 12)
            {
                LoopCount = 0;
                SwitchBox++;

            }

            //   GameManager.instance.TotalCountOxigen--;
            VibrationExample.instance.TapPeekVibrate();
            PassengersList[i].SetActive(true);
            PassengersList[i].GetComponent<NavMeshAgent>().speed = 20;
            SnakeMovement.Instance.tailObjects[/*SnakeMovement.Instance.tailObjects.Count -*/ SwitchBox].transform.GetChild(5).GetComponent<PassegersInSidetrain>().
              gameObject.transform.GetChild(LoopCount).gameObject.SetActive(false);
            yield return StartCoroutine(Lerp7());
        }

    }
    IEnumerator Lerp7()
    {
        yield return new WaitForSeconds(0.02f);
    }

    // Update is called once per frame
    void Update()
    {
        //g  if (GameManager.instance.TotalCountOxigen <= 0)
        //g   {
        //g    GameManager.instance.TotalCountOxigen = 0;

        //g }
        if (Input.GetMouseButton(1))
        {
            StartCoroutine(PassengersMove20());
        }
        else
        {
            //   Triggers.instance.SecondControl = false;
            PassengersStop();

        }
    }
}
