using DG.Tweening;
using GameAssets.GameSet.GameDevUtils.Managers;
using System.Collections;
using System.Collections.Generic;
using THEBADDEST.InteractSyetem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerDetection : TriggerEffector
{
    public Color white;
    public Color red;
    [SerializeField] public Outline outline;
    [SerializeField] int insectLevel;
    public Vector3 initialPosition;
    [SerializeField] bool canWalk;
    [SerializeField] Animator animator;
    public StateOfAI state;
    private Vector3 moveTarget;
    public bool isLineGenrated;
    THEBADDEST.CharacterController3.CharacterController characterController;
    LineRendererController rendererController;
    GameObject newLine;
    public GameObject characterHealthFillerBg;
    public Image characterHealthFiller;
    public Text characterHealthFillerText;
    internal ParticleSystem squidManBonePar;
    public bool openWorldEatableCheck = false;


    public bool firstInsectAiGroup = false;
    public bool secondInsectAiGroup = false;
    public bool thirdInsectAiGroup = false;
    public bool fourthInsectAiGroup = false;


    private void Awake()
    {
        GameController.onGameplay += OnGamePlay;
        initialPosition = transform.position;
        StartWalk();
    }

    private void Start()
    {
        if (characterHealthFillerBg)
            characterHealthFillerBg.SetActive(false);
    }

    IEnumerator DecreaseFillerOverTime()
    {
        while (characterHealthFiller.fillAmount > 0f)
        {
            int number = (int)characterController.currentLevel;

            if (number >= 20)
            {
                number = 20;
            }
            yield return new WaitForSeconds(0.1f - (number / 400));

            characterHealthFiller.fillAmount -= 0.1f;

            UpdateStatusText();

            if (characterHealthFiller.fillAmount <= 0)
            {
                JumpScenario();
            }
        }

        OnFillerEmpty();
    }

    void JumpBugs()
    {
        JumpScenario();
    }

    void UpdateStatusText()
    {
        int countdownValue = Mathf.CeilToInt(characterHealthFiller.fillAmount * 100);
        if (characterHealthFillerText) characterHealthFillerText.text = countdownValue.ToString() + "%";
    }

    void OnFillerEmpty()
    {
        if (characterHealthFillerBg) characterHealthFillerBg.SetActive(false);
    }

    protected override void OnEffect(Collider other, IEffectContainer container)
    {
        base.OnEffect(other, container);
        characterController = other.GetComponent<THEBADDEST.CharacterController3.CharacterController>();

        if (other.CompareTag("Player"))
        {
            if (this.GetComponentInParent<Collectable>().levelCount <= characterController.currentLevelCount)
            {
                if (!isLineGenrated && characterController.currentLevelCount >= 1)
                {
                    if (this.gameObject.GetComponentInParent<MoveTowardPlayer>())
                        this.gameObject.GetComponentInParent<MoveTowardPlayer>().enabled = false;

                    if (this.gameObject.GetComponentInParent<NavMeshAgent>())
                        this.gameObject.GetComponentInParent<NavMeshAgent>().enabled = false;

                    squidManBonePar = Instantiate(ReferenceManager.instance.characterController.squidManBoneParticle,
                    ReferenceManager.instance.characterController.squidManBoneParticleSpawnPoint.transform);

                    if (characterHealthFillerBg)
                    {
                        Debug.Log("Filler");
                        characterHealthFillerBg.SetActive(true);
                        StartCoroutine(DecreaseFillerOverTime());
                    }
                    else
                    {
                        Debug.Log("WithOut_Filler");
                        JumpBugs();
                    }

                    isLineGenrated = true;

                    newLine = Instantiate(characterController.lineRendererPrefeb, characterController.transform.position, Quaternion.identity);
                    rendererController = newLine.GetComponent<LineRendererController>();

                    rendererController.startPos = characterController.lineTarget;
                    rendererController.endPos = this.transform;

                    if (characterController.insects[characterController.currentLevelCount - 1].insect.GetComponentInChildren<SkinnedMeshRenderer>())
                        rendererController.lineRenderer.material = characterController.insects[characterController.currentLevelCount - 1].
                           insect.GetComponentInChildren<SkinnedMeshRenderer>().material;

                    rendererController.canDraw = true;

                    if (this.GetComponentInParent<Animator>())
                    {
                        this.GetComponentInParent<Animator>().SetTrigger("Run");
                    }
                    else
                    {
                        this.gameObject.transform.parent.GetComponentInChildren<Animator>().SetTrigger("Walk");
                    }
                    this.GetComponentInParent<Animator>().SetTrigger("Run");

                    if (this.transform.parent.parent.GetComponent<AiMovement>())
                        this.transform.parent.parent.GetComponent<AiMovement>().canMove = true;
                    if (this.transform.parent.parent.GetComponent<AiMovement>().canMove == true)
                        this.transform.parent.parent.GetComponent<AiMovement>().speed = 0.8f;

                    DOVirtual.DelayedCall(1.5f, () => Destroy(newLine));
                }
            }
            else
            {
                if (this.gameObject.GetComponentInParent<MoveTowardPlayer>())
                    this.gameObject.GetComponentInParent<MoveTowardPlayer>().enabled = false;

                if (this.gameObject.GetComponentInParent<NavMeshAgent>())
                    this.gameObject.GetComponentInParent<NavMeshAgent>().enabled = false;

                this.gameObject.transform.GetComponentInParent<Collectable>().gameObject.transform.
                    DOJump(characterController.transform.position, 2.5f, 1, 0.5f).SetEase(Ease.OutBack);
            }
        }
    }

    void JumpScenario()
    {
        this.transform.parent.parent.DOJump(characterController.transform.position, 5, 1, 0.25f).OnComplete(() =>
        {
            Destroy(newLine);
            squidManBonePar.Play();
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.squidManDieSound, 1);

            float destroyTime = 1.0f;

            Invoke("DestroyParticleSystem", destroyTime);

            rendererController.canDraw = false;
            this.transform.gameObject.GetComponentInParent<AiMovement>().gameObject.SetActive(false);
        });
    }

    void DestroyParticleSystem()
    {
        squidManBonePar.Stop();
        Destroy(squidManBonePar.gameObject);
    }

    public void StartWalk()
    {
        // canWalk = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            state = StateOfAI.BackToInitial;
            moveTarget = initialPosition;

            //outline.enabled = false;

        }
    }

    IEnumerator DelayForRandomWalk()
    {
        yield return new WaitForSeconds(3);
        state = StateOfAI.RandomWalk;
    }

    void OnGamePlay()
    {
        isLineGenrated = false;
    }
}

public enum StateOfAI
{
    RandomWalk,
    MoveToPlayer,
    BackToInitial
}
