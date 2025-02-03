using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using SWS;
using PathCreation.Examples;
public class SnakeMovement : MonoBehaviour
{
    public static SnakeMovement Instance;
    public int TotalSkipOxigen;
    public float speed = 2.0f;
    public float rotationSpeed = 180.0f;
    public string ColorName;

    public List<GameObject> tailObjects = new List<GameObject>();
    public GameObject tailPrefab;
    public GameObject CylendarsParent, BusCounter;
    public float nextStackPosition, stackOffset;
    public Transform Skeleton;
    public bool MainSnake1, MainSnake2, MainbSnake3;
    public Transform target;
    public splineMove SP1, SP2;

    public PathFollower1 pathFollwer1;
    public PathFollower1 pathFollwer2;

    // Use this for initialization
    public void Wait12()
    {
        SP1.GetComponent<splineMove>().Pause();
        SP2.GetComponent<splineMove>().Pause();

    }
    private void Awake()
    {
        BusCounter = GameObject.Find("BusCounter");
    }
    void Start()
    {

        a = 1;
        Instance = this;
        tailObjects.Add(gameObject);
        RacePlayerControler.instance.ChildFSpeed = 0;
        Invoke("Wait12", 0.2f);

        StartCoroutine(AddTail());
        tailObjects[1].transform.GetChild(5).GetComponent<PassegersInSidetrain>().Engine.SetActive(true);
        tailObjects[1].transform.GetChild(5).GetComponent<PassegersInSidetrain>().TrainBox.SetActive(false);

    }
    public void Addcylendar()
    {
        StartCoroutine(AddTail());
        Invoke("wait", 0.2f);

    }
    public void wait()
    {
        StartCoroutine(AddTail());


    }
    public int a;
    public void Wait()
    {
        RacePlayerControler.instance.ChildFSpeed = 0;

    }
    public void waitthis()
    {
        RacePlayerControler.instance.ChildFSpeed = 0;
    }

    public IEnumerator WaitSpeedDec()
    {
        for (int i = 0; i <= 9; i++)
        {
            tailObjects[i].GetComponent<TailMovement>().speed -= 2f;
            yield return new WaitForSeconds(12);
        }
    }

    public void StopPlayerWait2()
    {
        // if (GameManager.Instance.b == 1)
        //{
        //g  GameManager.instance.HoldUIActive();

        // }
        Triggers.instance.SecondControl = true;
        Triggers.instance.PickUp();
        for (int i = 1; i < tailObjects.Count; i++)
        {
            tailObjects[i].GetComponent<TailMovement>().speed = 0f;
            tailObjects[i].GetComponent<TailMovement>().Snake1 = false;
            tailObjects[i].transform.GetChild(5).GetComponent<PassegersInSidetrain>().DoorAnim();
        }

        pathFollwer1.speed = 0;
        pathFollwer2.speed = 0;

        RacePlayerControler.instance.ChildFSpeed = 0;
        SP1.GetComponent<splineMove>().Pause();
        SP2.GetComponent<splineMove>().Pause();
    }
    public void StopPlayerWait23()
    {
        Triggers.instance.SecondControl = true;
        for (int i = 1; i < tailObjects.Count; i++)
        {
            tailObjects[i].GetComponent<TailMovement>().speed = 0f;
            tailObjects[i].GetComponent<TailMovement>().Snake1 = false;
            tailObjects[i].transform.GetChild(5).GetComponent<PassegersInSidetrain>().DoorAnim();


        }
        pathFollwer1.speed = 0;
        pathFollwer2.speed = 0;

        RacePlayerControler.instance.ChildFSpeed = 0;
        SP1.GetComponent<splineMove>().Pause();
        SP2.GetComponent<splineMove>().Pause();
    }
    public void StopPlayer()
    {
        Invoke("StopPlayerWait2", 2);
        Triggers.instance.IsPlayerLerp = true;


    }
    public void StopPlayerLast()
    {
        Invoke("StopPlayerWait23", 2);
        Triggers.instance.IsPlayerLerp = true;


    }
    public void StopPlayerDestroytrain()
    {
        Invoke("StopPlayerWait2", 0);
        Triggers.instance.IsPlayerLerp = true;


    }
    public void StartPlayer()
    {
        for (int i = 1; i < tailObjects.Count; i++)
        {
            tailObjects[i].GetComponent<TailMovement>().enabled = true;
            tailObjects[i].GetComponent<TailMovement>().speed = 6f;
            tailObjects[i].GetComponent<TailMovement>().Snake1 = true;

            tailObjects[i].transform.GetChild(5).GetComponent<PassegersInSidetrain>().DoorClose();

        }

        pathFollwer1.speed = 20;
        pathFollwer2.speed = 20;

        RacePlayerControler.instance.ChildFSpeed = 0;
        SP1.GetComponent<splineMove>().Resume();
        SP2.GetComponent<splineMove>().Resume();


    }

    public void StartSubtractTrain()
    {

        StartCoroutine(SubtractTailFast());

    }
    public bool IsFailOnDes;
    void FixedUpdate()
    {
        //g if (GameManager.instance.Dum < 0 && GameManager.instance.IsComplete == false)
        {
            print("Destroy");
            IsFailOnDes = true;
            StartCoroutine(DestroyTrain());
            StopPlayer();

        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Invoke("waitthis", 0.4f);
            StartCoroutine(AddTail());
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            SP1.GetComponent<splineMove>().Pause();

        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            SP1.GetComponent<splineMove>().Resume();

        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            StopPlayer();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(SubtractTailFast());
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            RacePlayerControler.instance.ChildFSpeed = 0;

            StartCoroutine(SubtractTail2());
            Invoke("Wait", 0.1f);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(DestroyTrain());
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(DestroyTrainDum());
        }

        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
        }
    }

    public IEnumerator DestroyTrain()
    {
        for (int i = tailObjects.Count - 1; i >= 1; i--)
        {

            tailObjects[i].transform.GetChild(5).GetComponent<PassegersInSidetrain>().LoopOnBlendDestory();

            yield return null;
        }

        StartCoroutine(DestroyTrain2());
    }

    public IEnumerator DestroyTrain2()
    {
        yield return StartCoroutine(Lerp7());

        for (int i = tailObjects.Count - 1; i >= 1; i--)
        {
            print("List");
            tailObjects[i].transform.GetChild(5).GetComponent<PassegersInSidetrain>().TrainBodyOrg.SetActive(false);
            tailObjects[i].transform.GetChild(5).GetComponent<PassegersInSidetrain>().TrainBodyDestroy.SetActive(true);
            tailObjects[i].transform.GetChild(5).GetComponent<PassegersInSidetrain>().RagDollActivate();
        }
    }

    public IEnumerator DestroyTrainDum()
    {
        for (int i = tailObjects.Count - 1; i >= 1; i--)
        {
            //g  GameManager.instance.TotalCountOxigen--;

            tailObjects[i].transform.GetChild(5).GetComponent<PassegersInSidetrain>().LoopOnBlendDestory();
            BusCounter.GetComponent<ToatalPassengersControler>().Destroy();
            yield return StartCoroutine(Lerp7());
        }

        StartCoroutine(DestroyTrainDum2());
    }
    public IEnumerator DestroyTrainDum2()
    {

        for (int i = tailObjects.Count - 1; i >= 1; i--)
        {
            print("List");
            tailObjects[i].transform.GetChild(5).GetComponent<PassegersInSidetrain>().TrainBodyOrg.SetActive(false);
            tailObjects[i].transform.GetChild(5).GetComponent<PassegersInSidetrain>().TrainBodyDestroy.SetActive(true);
            tailObjects[i].transform.GetChild(5).GetComponent<PassegersInSidetrain>().RagDollActivate();
            BusCounter.GetComponent<ToatalPassengersControler>().Destroy();

            yield return StartCoroutine(Lerp6());
        }


    }
    public GameObject Cylendar, Mover2;
    public float mover;
    public int BoxInCount;
    public IEnumerator AddTail()
    {
        mover = -12f;

        Mover2.transform.localPosition = new Vector3(0, 0, Mover2.transform.localPosition.z + mover);
        //g GameManager.instance.TotalCountOxigen = GameManager.instance.TotalCountOxigen + 11;
        Vector3 newTailPosition = Mover2.transform.localPosition;

        Vector3 newTailPosition2 = tailObjects[tailObjects.Count - 1].transform.localPosition;
        rotationSpeed += 5.0f;
        BoxInCount = BoxInCount + 1;
        Cylendar = GameObject.Instantiate(tailPrefab, newTailPosition2, Quaternion.identity) as GameObject;
        Cylendar.name = "TrainBox" + (BoxInCount);
        Cylendar.transform.parent = CylendarsParent.transform;

        tailObjects.Add(Cylendar);
        //g GameManager.instance.TextAnim.GetComponent<Animation>().Play();
        Cylendar.transform.localPosition = /*new Vector3(0, 0, */newTailPosition2;

        for (int i = 1; i < tailObjects.Count; i++)
        {
            print("List");
            tailObjects[i].transform.GetChild(4).GetComponent<Animation>().Play();
            yield return StartCoroutine(Lerp6());
        }
        for (int i = 1; i < tailObjects.Count; i++)
        {
            //print("List");
            tailObjects[i].transform.GetChild(0).GetComponent<Animation>().Play(ColorName);
            tailObjects[i].transform.GetChild(1).GetComponent<Animation>().Play(ColorName);

            yield return StartCoroutine(Lerp5());
        }
        if (a == 1)
        {
            tailObjects[1].transform.GetChild(3).gameObject.SetActive(true);
            a = 2;
        }
    }
    public IEnumerator SubtractTail()
    {


        if (tailObjects.Count > 1)
        {
            yield return new WaitForSeconds(1f);
        }
    }

    public IEnumerator SubtractTailFast()
    {
        if (tailObjects.Count > 1)
        {
            VibrationExample.instance.TapPopVibrate();

            //g  GameManager.instance.TextAnim.GetComponent<Animation>().Play();
            //g  GameManager.instance.CloudPArticlesFun();

            if (MainSnake1 == true)
                tailObjects[2].transform.parent = null;

            tailObjects[2].transform.GetComponent<TailMovement>().enabled = false;
            tailObjects[2].gameObject.SetActive(false);

            tailObjects[3].GetComponent<TailMovement>().tailTarget = tailObjects[1];
            tailObjects.RemoveAt(2);

            //g GameManager.instance.TotalCountOxigen -= 11;

            for (int i = 1; i < tailObjects.Count; i++)
            {
                tailObjects[i].transform.GetChild(0).GetComponent<Animation>().Play(ColorName);
                tailObjects[i].transform.GetChild(1).GetComponent<Animation>().Play(ColorName);

                yield return StartCoroutine(Lerp5());
            }
            if (a == 1)
            {
                tailObjects[2].transform.GetChild(3).gameObject.SetActive(true);
                a = 2;
            }

            yield return new WaitForSeconds(2f);
        }
    }

    public IEnumerator SubtractTail2()
    {
        tailObjects[2].transform.parent = null;
        tailObjects[2].transform.GetComponent<TailMovement>().enabled = false;
        tailObjects.RemoveAt(2);
        //g  GameManager.instance.TotalCountOxigen--;

        yield return null;
    }

    public void Deactivate()
    {
        for (int i = 1; i < tailObjects.Count; i++)
        {
            //tailObjects[i].GetComponent<TailMovement>().enabled=false;
        }

    }
    public void Activate()
    {
        for (int i = 1; i < tailObjects.Count; i++)
        {
            //tailObjects[i].GetComponent<TailMovement>().enabled = true;
        }

    }

    void Reposition()
    {
        //for (int i = 0; i < tailObjects.Count; i++)
        //{
        //    nextStackPosition = i * stackOffset;
        //    tailObjects[i].transform.localPosition = new Vector3(tailObjects[i].transform.localPosition.x,
        //    tailObjects[i].transform.localPosition.y,
        //        nextStackPosition);
        //}
        //stackContainer.localPosition = new Vector3(0, stackContainer.localPosition.y, nextStackPosition);
        //set targets

        //SetTargets();
    }

    IEnumerator Lerp5()
    {
        yield return new WaitForSeconds(0.002f);
    }
    IEnumerator Lerp7()
    {
        yield return new WaitForSeconds(1.1f);
    }
    IEnumerator Lerp8()
    {
        yield return new WaitForSeconds(0.6f);
    }
    IEnumerator Lerp6()
    {
        yield return new WaitForSeconds(0.1f);
    }
}
