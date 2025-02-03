using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour
{
    public static Triggers instance;
    // Start is called before the first frame update
    public SnakeMovement MainSnake1, MainStake2, MainStake3;
    public GameObject CamStstes, UpTrack, SkellActivate, Target;
    public GameObject AICollider1, AICollider2, DollerIns, Boy1Naked, Boy1Cloth, Boy2Naked, Boy2Cloth, Girl1Naked, Girl1Cloth, Girl2Naked, Girl2Cloth, Lastrow, Confetty;
    public bool IsPlayerLerp, SecondControl;
    public Transform TargetLast;
    public int TotalPassengersIn;
    public GameObject CoinPickParticles, CanvasText;
    void Awake()
    {
        CamStstes = GameObject.FindGameObjectWithTag("Camera");
        // Lastrow = GameObject.FindGameObjectWithTag("LastRow");
        instance = this;
        // Lastrow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameManager.instance.Activatetriggers == true)
        //{
        //    AICollider1.SetActive(true);
        //    AICollider2.SetActive(true);

        //}
        //if (GameManager.instance.Activatetriggers == false)
        //{
        //    AICollider1.SetActive(false);
        //    AICollider2.SetActive(false);

        //}
        if (Input.GetKeyDown(KeyCode.J))
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;

            gameObject.GetComponent<RacePlayerControler>().Jump(5);
            Invoke("UseGravity", 3);

        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            SnakeMovement.Instance.StopPlayer();

        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            SnakeMovement.Instance.StartPlayer();
            Triggers.instance.SecondControl = false;
        }
    }
    public void StartPlayer()
    {
        SnakeMovement.Instance.StartPlayer();
        Triggers.instance.SecondControl = false;

    }
    public void AddCoin(Transform CoinPos)
    {
        GameObject P = Instantiate(CoinPickParticles) as GameObject;
        P.transform.position = CoinPos.position;
        //ReferenceManager.instance._UI_Handler.AddCoin();
        //P.transform.rotation = CoinPos.rotation;
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            IsPlayerLerp = true;

        }
        if (IsPlayerLerp)
        {
            MainSnake1.transform.localPosition = Vector3.Lerp(MainSnake1.transform.localPosition, new Vector3(TargetLast.transform.localPosition.x, MainSnake1.transform.localPosition.y, MainSnake1.transform.localPosition.z), Time.fixedDeltaTime * 8);
        }
    }
    public void UseGravity()
    {
        // IsJump = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;

    }
    public Transform thiss;
    public bool IsDestroyCylendars;
    public void PickUp()
    {

        CamStstes.GetComponent<Animator>().SetBool("PassengersBool", true);
        CamStstes.GetComponent<Animator>().SetBool("IdleBool", false);

    }
    public void Last()
    {

        CamStstes.GetComponent<Animator>().SetBool("PassengersBool", false);
        CamStstes.GetComponent<Animator>().SetBool("IdleBool", false);
        CamStstes.GetComponent<Animator>().SetBool("FinalCam", true);

    }
    public void Pickrelease()
    {

        CamStstes.GetComponent<Animator>().SetBool("PassengersBool", false);
        CamStstes.GetComponent<Animator>().SetBool("IdleBool", true);

    }
    public void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Hurdles")
        {
            // yield return new WaitForSeconds(2f);

            //SnakeMovement.Instance.StopPlayerDestroytrain();

            //  StartCoroutine(SnakeMovement.Instance.SubtractTailFast());
            IsDestroyCylendars = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hurdles")
        {
            if (SnakeMovement.Instance.tailObjects.Count <= 2)
            {
                //SnakeMovement.Instance.tailObjects[1].transform.GetChild(5).GetComponent<PassegersInSidetrain>().Enginedes();
                // SnakeMovement.Instance.StopPlayerWait2();
                // GameManager.instance.FAilFun2();
                // Triggers.instance.FinalDes = true;

            }
            else
            {
                //  SnakeMovement.Instance.StartPlayer();
                IsDestroyCylendars = false;
            }

        }
        if (other.gameObject.name == "Skeleton")
        {
            print("Ex");
            SnakeMovement.Instance.StartPlayer();

            RacePlayerControler.instance.IsTriggerOnSkeleton = false;

            RacePlayerControler.instance.ChildFSpeed = 0;
            MainSnake1.Activate();
            CamStstes.GetComponent<Animator>().SetBool("IdleBool", true);
            CamStstes.GetComponent<Animator>().SetBool("SkeletonBool", false);
            IsDestroyCylendars = false;


        }
    }
    public void InstaniateBoy1Naked()
    {
        Instantiate(Boy1Naked, Target.transform.position, Target.transform.rotation);
        DollerIns.SetActive(true);

    }
    public void InstaniateBoy1Cloth()
    {
        Instantiate(Boy1Cloth, Target.transform.position, Target.transform.rotation);

        DollerIns.SetActive(true);

    }
    public void InstaniateBoy2Naked()
    {
        Instantiate(Boy2Naked, Target.transform.position, Target.transform.rotation);

        DollerIns.SetActive(true);

    }
    public void InstaniateBoy2Cloth()
    {
        Instantiate(Boy2Cloth, Target.transform.position, Target.transform.rotation);

        DollerIns.SetActive(true);

    }
    public void InstaniateGirl1Naked()
    {
        Instantiate(Girl1Naked, Target.transform.position, Target.transform.rotation);
        DollerIns.SetActive(true);


    }
    public void InstaniateGirl1Cloth()
    {
        Instantiate(Girl1Cloth, Target.transform.position, Target.transform.rotation);
        DollerIns.SetActive(true);


    }
    public void InstaniateGirl2Naked()
    {
        Instantiate(Girl2Naked, Target.transform.position, Target.transform.rotation);

        DollerIns.SetActive(true);

    }
    public void InstaniateGirl2Cloth()
    {
        Instantiate(Girl2Cloth, Target.transform.position, Target.transform.rotation);

        DollerIns.SetActive(true);

    }

    public void WaitToOffPlayer()
    {
        //  PlayerControler.instance.ChildFSpeed = 0;

    }
    public void WaitToOffPlayerEnd()
    {


        Confetty.SetActive(true);
    }
    public void LastCam()
    {
        CamStstes.GetComponent<Animator>().SetBool("LastCam", true);


    }
    public void FailControler()
    {
        gameObject.GetComponent<ThirdPersonUserControl>().IsControlerActive = false;
        RacePlayerControler.instance.ChildFSpeed = 0;
        SnakeMovement.Instance.SP1.Stop();
        SnakeMovement.Instance.SP2.Stop();


    }
    public void wait()
    {
        //if (GameManager.instance.PAssengerCOuntForEnd <= 20)
        //{
        //    StartCoroutine(ToatalPassengersControler1.instance.PassengersMove20());
        //    GameManager.instance.CompleteFun(5);

        //}
        //if (GameManager.instance.PAssengerCOuntForEnd > 20 && GameManager.instance.PAssengerCOuntForEnd <= 45)
        //{
        //    StartCoroutine(ToatalPassengersControler1.instance.PassengersMove45());
        //    GameManager.instance.CompleteFun(8);

        //}
        //if (GameManager.instance.PAssengerCOuntForEnd > 45 && GameManager.instance.PAssengerCOuntForEnd <= 200)
        //{
        //    StartCoroutine(ToatalPassengersControler1.instance.PassengersMove70());
        //    GameManager.instance.CompleteFun(10);

        //}

    }
    public bool FinalDes;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hurdles")
        {
            if (SnakeMovement.Instance.tailObjects.Count <= 3)
            {
                SnakeMovement.Instance.tailObjects[1].transform.GetChild(5).GetComponent<PassegersInSidetrain>().Enginedes();
                SnakeMovement.Instance.tailObjects[2].SetActive(false);
                // SnakeMovement.Instance.tailObjects[1].SetActive(false);


                SnakeMovement.Instance.StopPlayerWait2();
                SnakeMovement.Instance.StopPlayer();

                //g GameManager.instance.FAilFun2();
                Triggers.instance.FinalDes = true;

            }
            else
            {
                SimpleCameraShakeInCinemachine.instance.ShakeIt();
                StartCoroutine(SnakeMovement.Instance.SubtractTailFast());
                IsDestroyCylendars = true;
            }

        }
        if (other.gameObject.name == "SpeedUp")
        {
            Time.timeScale = 1.6f;
        }

        if (other.gameObject.name == "SpeedDown")
        {
            Time.timeScale = 1.2f;
        }
        if (other.gameObject.name == "PickUpPoint")
        {
            //   print("stay");
            SnakeMovement.Instance.StopPlayer();

            PickUp();


            // IsDestroyCylendars = true;
        }
        if (other.gameObject.name == "LastPoint")
        {
            //   print("stay");
            Invoke("wait", 2.5f);
            Last();
            FinalDes = true;
            CanvasText.SetActive(false);
            SecondControl = true;
            SnakeMovement.Instance.StopPlayerLast();
            //  PickUp();


            // IsDestroyCylendars = true;
        }
        if (other.gameObject.tag == "Coins")
        {
            //   print("stay");
            AddCoin(other.transform);
            other.gameObject.SetActive(false);

            // IsDestroyCylendars = true;
        }
        if (other.gameObject.name == "LerpPlayer")
        {
            IsPlayerLerp = true;
        }
        if (other.gameObject.tag == "Passengers")
        {
            TotalPassengersIn++;
            other.gameObject.SetActive(false);

        }
        if (other.gameObject.name == "FinishPlayer")
        {
            //  RaceLevelsManager.Instance.IsWayPoint = false;
            IsPlayerLerp = true;
            gameObject.GetComponent<ThirdPersonUserControl>().IsControlerActive = false;
            RacePlayerControler.instance.ChildFSpeed = 0;
            SnakeMovement.Instance.SP1.Stop();
            SnakeMovement.Instance.SP2.Stop();
            //SnakeMovement.Instance.SP1.speed = 2;
            // SnakeMovement.Instance.SP2.speed = 2;

            CamStstes.GetComponent<Animator>().SetBool("FinishCam", true);
            Lastrow.SetActive(true);
            Invoke("LastCam", 2.5f);
            Invoke("WaitToOffPlayer", 2);
            Invoke("WaitToOffPlayerEnd", 8);
            //g  GameManager.instance.CompleteFun(15);
        }
        if (other.gameObject.tag == "Cylendar")
        {
            MainSnake1.Addcylendar();
            RacePlayerControler.instance.PlayerPointText.gameObject.SetActive(true);
            // MainStake2.Addcylendar();
            //MainStake3.Addcylendar();
            other.gameObject.GetComponent<Animation>().Play("DownThis");
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            other.gameObject.SetActive(false);
            VibrationExample.instance.TapPeekVibrate();

        }
        if (other.gameObject.name == "UpThisTrack")
        {
            //   UpTrack.GetComponent<Animator>().enabled = true;
        }
        if (other.gameObject.name == "Jump")
        {
            //  IsJump = true;
            gameObject.GetComponent<Rigidbody>().useGravity = true;

            gameObject.GetComponent<RacePlayerControler>().Jump(4);
            Invoke("UseGravity", 3);

        }
        if (other.gameObject.name == "oxigenBlue")
        {
            MainSnake1.ColorName = "oxigenBlue";
            //MainStake2.ColorName = "oxigenBlue";
            //MainStake3.ColorName = "oxigenBlue";

            MainSnake1.Addcylendar();
            // MainStake2.Addcylendar();
            // MainStake3.Addcylendar();

        }
        if (other.gameObject.name == "oxigenGreen")
        {
            MainSnake1.ColorName = "oxigenGreen";
            // MainStake2.ColorName = "oxigenGreen";
            // MainStake3.ColorName = "oxigenGreen";

            MainSnake1.Addcylendar();
            //  MainStake2.Addcylendar();
            //  MainStake3.Addcylendar();

        }
        if (other.gameObject.name == "oxigenOrange")
        {
            MainSnake1.ColorName = "oxigenOrange";
            //   MainStake2.ColorName = "oxigenOrange";
            //   MainStake3.ColorName = "oxigenOrange";

            MainSnake1.Addcylendar();
            //  MainStake2.Addcylendar();
            //   MainStake3.Addcylendar();

        }
        if (other.gameObject.name == "Skeleton")
        {
            //MainSnake1.StartCoroutine();

        }
    }
}
