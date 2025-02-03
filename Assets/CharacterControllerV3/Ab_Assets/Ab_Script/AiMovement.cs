using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using THEBADDEST.InteractSyetem;
using MergeSystem;
using UnityEngine.EventSystems;

public class AiMovement : MonoBehaviour
{
    [SerializeField] int index;
    public GameObject aiSponPoint;
    [SerializeField] List<Transform> sponPoint = new List<Transform>();
    [SerializeField] public bool canMove;
    [SerializeField] bool canRoate;
    bool gameStarted;
    public float speed;
    [SerializeField] Transform child;
    Vector3 destination;
    [Space(10)]
    [SerializeField] internal LayerMask layerMask;
    RaycastHit hit;//For Detect Sureface/Base.
    Vector3 surfaceNormal;//The normal of the surface the ray hit.
    Vector3 forwardRelativeToSurfaceNormal;//For Look Rotation
    Collectable collectable;
    [SerializeField] float CurrentAngle;

    private void Awake()
    {
        gameStarted = true;
    }

    void Start()
    {
        //layerMask = LayerMask.NameToLayer("Ground");
        collectable = GetComponent<Collectable>();
    }

    public void CharacterMove()
    {
        if (!gameStarted) return;

        if (canMove)
        {
            RandomWalk();
        }
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1);
        canMove = true;
        this.GetComponent<Animator>().SetTrigger("Walk");
        CharecterConstraints();
        Rotation();
    }

    void CharecterConstraints()
    {
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<Collider>().isTrigger = true;
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        Destroy(this.GetComponent<Rigidbody>(), 0.5f);
    }

    public void Rotation()
    {
        if (!canRoate)
        {
            canRoate = true;
            transform.DOLocalRotate(new Vector3(0, Random.Range(180, -180), 0), 1, RotateMode.LocalAxisAdd).SetRelative();
            //Debug.Log("Rotate");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Planet"))
        {
            //Debug.Log("Planet");
            StartCoroutine(Spawn());
        }
        if (other.gameObject.GetComponent<Collectable>())
        {
            //Debug.Log("AiHit");

            if (CheckAngle(other.transform))
            {
                //Debug.Log("AiHit___01");
                OnAiCollection(other.gameObject);
            }
        }
    }

    void OnAiCollection(GameObject AiInsect)
    {
        if (AiInsect)
        {
            if (AiInsect.GetComponent<Collectable>() && collectable)
            {
                var Ai = AiInsect.GetComponent<Collectable>();

                if (collectable.levelCount > Ai.levelCount)
                {
                    Ai.antData.transform.DOScale(0, 0.3f).SetEase(Ease.OutBounce);
                    Ai.GetComponent<Rigidbody>().isKinematic = true;
                    Ai.GetComponent<Collider>().enabled = false;
                    //Ai.ResetInsect();

                    //ParticleManager.Instance.PlaceParticle(Ai.transform);
                }
                else
                {
                    canRoate = false;
                }
            }
        }
    }

    void RandomWalk()
    {
        //Debug.Log("RandomWalk");
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

    }

    bool CheckAngle(Transform ai)
    {
        Vector3 targetDir = ai.position - transform.position;
        Vector3 forward = transform.forward;
        float angle = Vector3.SignedAngle(targetDir, forward, Vector3.up);
        CurrentAngle = angle;
        if (angle < -30f || angle < 30F)
            return true;
        else
            return false;
    }
}
