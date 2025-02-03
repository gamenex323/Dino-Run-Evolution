using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TailMovement : MonoBehaviour
{
    public float speed;
    public string PlayerTag;
    public GameObject tailTarget, OrnagePAty, GreenParty, YellowPArty;
    public Vector3 tailTargetPosition, tailTargetPosition2;

    public SnakeMovement mainSnake1, mainSnake2, mainSnake3, MainSkeleton;
    public bool Snake1, Snake2, Snake3, Skeleton;
    public GameObject animation;

    // Use this for initialization
    void Start()
    {
        if (Snake1)
        {
            speed = 6;
            mainSnake1 = GameObject.FindWithTag(PlayerTag).GetComponent<SnakeMovement>();
            tailTarget = mainSnake1.tailObjects[mainSnake1.tailObjects.Count - 2];
            //numberTarget = mainSnake1.tailObjects.IndexOf(gameObject)
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Snake1)
        {
            tailTargetPosition = new Vector3(tailTarget.transform.position.x, tailTarget.transform.position.y, tailTarget.transform.position.z /*+ mainSnake1.mover*/);
            //tailTargetPosition2 = new Vector3(tailTarget.transform.localPosition.x, tailTarget.transform.localPosition.y, tailTarget.transform.localPosition.z + mainSnake1.mover);
            transform.LookAt(tailTargetPosition);
            transform.position = Vector3.Lerp(transform.position, tailTargetPosition, Time.fixedDeltaTime * speed);
        }
    }

    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Skeleton" && Triggers.instance.IsDestroyCylendars == true)
        {
            //print("SkelettonCount");
            //yield return new WaitForSeconds(0.05f);
            StartCoroutine(SnakeMovement.Instance.SubtractTail());

            yield return new WaitForSeconds(0.08f);

            StartCoroutine(SnakeMovement.Instance.SubtractTailFast());
            VibrationExample.instance.TapPopVibrate();

        }
        if (other.gameObject.tag == "Hurdles" /*&& Triggers.instance.IsDestroyCylendars == true*/)
        {
            VibrationExample.instance.TapPopVibrate();
        }
        if (other.gameObject.name == "oxigenBlue")
        {

            animation.GetComponent<Animation>().Play("oxigenBlue");
            YellowPArty.SetActive(true);
            VibrationExample.instance.TapPeekVibrate();
        }
        if (other.gameObject.name == "oxigenGreen")
        {
            animation.GetComponent<Animation>().Play("oxigenGreen");
            GreenParty.SetActive(true);
            VibrationExample.instance.TapPeekVibrate();
        }
        if (other.gameObject.name == "oxigenOrange")
        {
            animation.GetComponent<Animation>().Play("oxigenOrange");
            OrnagePAty.SetActive(true);

            VibrationExample.instance.TapPeekVibrate();
        }
    }
}
