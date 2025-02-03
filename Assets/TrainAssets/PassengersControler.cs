using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PassengersControler : MonoBehaviour
{
    public List<GameObject> PassengersList = new List<GameObject>();
    public int CounterPassengers;
    public Transform Parent;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void AddPassengers(GameObject Obj)
    {
        PassengersList.Add(Obj);

        for (int i = 1; i < PassengersList.Count; i++)
        {
            print("List");
            //  PassengersList[i].transform.GetChild(4).GetComponent<Animation>().Play();
            //  yield return StartCoroutine(Lerp6());
        }

    }
    public int Box1, Box2, Box3, Box4, Box5, Box6, Box7, Box8, Box9, Box10, Box11, Box12, Box13, Box14, Box15, Box16, Box17, Box18, Box19, Box20;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Passengers")
        {
            VibrationExample.instance.TapPeekVibrate();
            //g GameManager.instance.PAssengerCOuntForEnd++;

            //g GameManager.instance.TotalCountOxigen--;
            //g  if (GameManager.instance.TotalCountOxigen <= 1)
            {
                //g   GameManager.instance.TotalCountOxigen = 0;

            }
            Triggers.instance.TotalPassengersIn++;
            other.gameObject.SetActive(false);

            if (Triggers.instance.TotalPassengersIn <= 12)
            {

                SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 1].transform.GetChild(5).GetComponent<PassegersInSidetrain>().
                gameObject.transform.GetChild(Box1).gameObject.SetActive(true);
                if (Box1 == 6)
                {
                    SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 1].transform.GetChild(5).GetComponent<PassegersInSidetrain>().LoopOnBlend();
                    //  SnakeMovement.Instance.tailObjects[1].transform.GetChild(5).GetComponent<PassegersInSidetrain>().LoopOnBlend();

                }
                Box1++;


            }

            if (Triggers.instance.TotalPassengersIn >= 12 && Triggers.instance.TotalPassengersIn <= 23)
            {


                SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 2].transform.GetChild(5).GetComponent<PassegersInSidetrain>().
            gameObject.transform.GetChild(Box2).gameObject.SetActive(true);
                if (Box2 == 6)
                {
                    SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 2].transform.GetChild(5).GetComponent<PassegersInSidetrain>().LoopOnBlend();

                }
                Box2++;

            }
            if (Triggers.instance.TotalPassengersIn >= 22 && Triggers.instance.TotalPassengersIn <= 33)
            {


                SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 3].transform.GetChild(5).GetComponent<PassegersInSidetrain>().
            gameObject.transform.GetChild(Box3).gameObject.SetActive(true);
                if (Box3 == 6)
                {
                    SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 3].transform.GetChild(5).GetComponent<PassegersInSidetrain>().LoopOnBlend();

                }
                Box3++;
            }
            if (Triggers.instance.TotalPassengersIn >= 32 && Triggers.instance.TotalPassengersIn <= 43)
            {


                SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 4].transform.GetChild(5).GetComponent<PassegersInSidetrain>().
            gameObject.transform.GetChild(Box4).gameObject.SetActive(true);
                if (Box4 == 6)
                {
                    SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 4].transform.GetChild(5).GetComponent<PassegersInSidetrain>().LoopOnBlend();

                }
                Box4++;
            }
            if (Triggers.instance.TotalPassengersIn >= 42 && Triggers.instance.TotalPassengersIn <= 53)
            {


                SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 5].transform.GetChild(5).GetComponent<PassegersInSidetrain>().
            gameObject.transform.GetChild(Box5).gameObject.SetActive(true);
                if (Box5 == 6)
                {
                    SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 5].transform.GetChild(5).GetComponent<PassegersInSidetrain>().LoopOnBlend();

                }
                Box5++;
            }
            if (Triggers.instance.TotalPassengersIn >= 52 && Triggers.instance.TotalPassengersIn <= 63)
            {


                //  SnakeMovement.Instance.tailObjects[7].transform.GetChild(5).GetComponent<PassegersInSidetrain>().
                SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 6].transform.GetChild(5).GetComponent<PassegersInSidetrain>().
              gameObject.transform.GetChild(Box6).gameObject.SetActive(true);
                if (Box6 == 6)
                {
                    SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 6].transform.GetChild(5).GetComponent<PassegersInSidetrain>().LoopOnBlend();

                }
                Box6++;

            }
            if (Triggers.instance.TotalPassengersIn >= 62 && Triggers.instance.TotalPassengersIn <= 73)
            {


                //  SnakeMovement.Instance.tailObjects[7].transform.GetChild(5).GetComponent<PassegersInSidetrain>().
                SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 7].transform.GetChild(5).GetComponent<PassegersInSidetrain>().
              gameObject.transform.GetChild(Box7).gameObject.SetActive(true);
                if (Box7 == 6)
                {
                    SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 7].transform.GetChild(5).GetComponent<PassegersInSidetrain>().LoopOnBlend();

                }
                Box7++;

            }
            if (Triggers.instance.TotalPassengersIn >= 72 && Triggers.instance.TotalPassengersIn <= 83)
            {


                //  SnakeMovement.Instance.tailObjects[7].transform.GetChild(5).GetComponent<PassegersInSidetrain>().
                SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 8].transform.GetChild(5).GetComponent<PassegersInSidetrain>().
              gameObject.transform.GetChild(Box8).gameObject.SetActive(true);
                if (Box8 == 6)
                {
                    SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 8].transform.GetChild(5).GetComponent<PassegersInSidetrain>().LoopOnBlend();

                }
                Box8++;

            }
            if (Triggers.instance.TotalPassengersIn >= 82 && Triggers.instance.TotalPassengersIn <= 93)
            {


                //  SnakeMovement.Instance.tailObjects[7].transform.GetChild(5).GetComponent<PassegersInSidetrain>().
                SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 9].transform.GetChild(5).GetComponent<PassegersInSidetrain>().
              gameObject.transform.GetChild(Box9).gameObject.SetActive(true);
                if (Box9 == 6)
                {
                    SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 9].transform.GetChild(5).GetComponent<PassegersInSidetrain>().LoopOnBlend();

                }
                Box9++;

            }
            if (Triggers.instance.TotalPassengersIn >= 92 && Triggers.instance.TotalPassengersIn <= 103)
            {


                //  SnakeMovement.Instance.tailObjects[7].transform.GetChild(5).GetComponent<PassegersInSidetrain>().
                SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 10].transform.GetChild(5).GetComponent<PassegersInSidetrain>().
              gameObject.transform.GetChild(Box10).gameObject.SetActive(true);
                if (Box10 == 6)
                {
                    SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 10].transform.GetChild(5).GetComponent<PassegersInSidetrain>().LoopOnBlend();

                }
                Box10++;

            }


            if (Triggers.instance.TotalPassengersIn >= 102 && Triggers.instance.TotalPassengersIn <= 113)
            {


                SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 11].transform.GetChild(5).GetComponent<PassegersInSidetrain>().
            gameObject.transform.GetChild(Box11).gameObject.SetActive(true);
                if (Box11 == 6)
                {
                    SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 11].transform.GetChild(5).GetComponent<PassegersInSidetrain>().LoopOnBlend();

                }
                Box11++;

            }
            if (Triggers.instance.TotalPassengersIn >= 112 && Triggers.instance.TotalPassengersIn <= 123)
            {


                SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 12].transform.GetChild(5).GetComponent<PassegersInSidetrain>().
            gameObject.transform.GetChild(Box12).gameObject.SetActive(true);
                if (Box12 == 6)
                {
                    SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 12].transform.GetChild(5).GetComponent<PassegersInSidetrain>().LoopOnBlend();

                }
                Box12++;
            }
            if (Triggers.instance.TotalPassengersIn >= 122 && Triggers.instance.TotalPassengersIn <= 133)
            {


                SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 13].transform.GetChild(5).GetComponent<PassegersInSidetrain>().
            gameObject.transform.GetChild(Box13).gameObject.SetActive(true);
                if (Box13 == 6)
                {
                    SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 13].transform.GetChild(5).GetComponent<PassegersInSidetrain>().LoopOnBlend();

                }
                Box13++;
            }
            if (Triggers.instance.TotalPassengersIn >= 132 && Triggers.instance.TotalPassengersIn <= 143)
            {
                SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 14].transform.GetChild(5).GetComponent<PassegersInSidetrain>().
                gameObject.transform.GetChild(Box14).gameObject.SetActive(true);
                if (Box14 == 6)
                {
                    SnakeMovement.Instance.tailObjects[SnakeMovement.Instance.tailObjects.Count - 14].transform.GetChild(5).GetComponent<PassegersInSidetrain>().LoopOnBlend();

                }
                Box14++;
            }



        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Passengers")
        {

            other.transform.parent = Parent.transform;
            //PAssengerCOuntForEnd
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Passengers")
        {
            print("ddd");
            other.transform.parent = null;

        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
