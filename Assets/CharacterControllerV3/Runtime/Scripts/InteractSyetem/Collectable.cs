using UnityEngine;
using DG.Tweening;
using System.Collections;
using System;
using GameAssets.GameSet.GameDevUtils.Managers;
using UnityEngine.UI;
using THEBADDEST.CharacterController3;

namespace THEBADDEST.InteractSyetem
{
    public class Collectable : TriggerEffector
    {
        [SerializeField] SkinnedMeshRenderer meshRenderer;
        public CollectionType collectionType;
        public LevelType level;
        [SerializeField] Text levelNumberText;
        [SerializeField] Text levelText;
        public int levelCount;
        [SerializeField] GameObject x1Point;
        [SerializeField] GameObject x2Point;
        [SerializeField] ParticleSystem Conff;
        [SerializeField] GameObject spider;
        [SerializeField] bool isLizardScene;
        THEBADDEST.CharacterController3.CharacterController characterController;


        private PlayerDetection playerDetection;
        [SerializeField] public GameObject antData;
        bool canTake = false;
        public GameObject endPointPart;
        public float fallForce;
        private void Awake()
        {
            THEBADDEST.CharacterController3.CharacterController.onCharacterUpdate += UpdateTextColor;
        }

        private void Start()
        {
            if (levelCount <= 9)
            {
                if (levelNumberText) levelNumberText.text = "0" + levelCount.ToString();

                if (isLizardScene)
                {
                    levelCount = 1;
                }
            }
            else
            {
                if (levelNumberText) levelNumberText.text = levelCount.ToString();
                if (isLizardScene)
                {

                    levelCount = 1;
                }
            }
        }

        DOTweenAnimation dOTweenAnimation;

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Legs"))
            {
                Destroy(this.GetComponent<Collider>());
                transform.parent = other.transform;
                transform.localPosition = Vector3.zero;
                dOTweenAnimation = other.GetComponentInParent<DOTweenAnimation>();
                dOTweenAnimation.DORestartAllById("L_P");

                this.transform.DOScale(0f, 1f).OnComplete(() =>
                {
                    ReferenceManager.instance.characterController.lizardCollection.Play();
                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.eatInsect, 1);
                    Destroy(this.gameObject);
                });
                if (collectionType == CollectionType.onEndPoint)
                {
                    CoinsManager.Instance.AddCoins(WordPointToCanvasPoint(CoinsManager.Instance.mainCam, transform.position, CoinsManager.Instance.canvasRect), 1);
                    CoinsManager.Instance.AddCoins(1);
                    if (x1Point)
                        x1Point.GetComponent<DOTweenAnimation>().DORestartById("Pop");
                    if (x2Point)
                        x2Point.GetComponent<DOTweenAnimation>().DORestartById("Pop");

                    if (Conff)
                        Conff.Play();
                }
            }
            if (other.CompareTag("Player"))
            {
                characterController = other.GetComponent<THEBADDEST.CharacterController3.CharacterController>();

                if (collectionType == CollectionType.onGround)
                {
                    GroundAction();
                }
                else
                {
                    EndPointAction();
                }
            }
        }

        public Vector2 WordPointToCanvasPoint(Camera camera, Vector3 worldPoint, RectTransform canvasRect)
        {
            Vector2 viewportPosition = camera.WorldToViewportPoint(worldPoint);
            Vector2 screenPosition = new Vector2(((viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)), ((viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));
            return screenPosition;
        }

        void EndPointAction()
        {
            if (levelCount <= characterController.currentLevelCount)
            {
                EndPointFallEffect();
                CoinsManager.Instance.AddCoins(WordPointToCanvasPoint(CoinsManager.Instance.mainCam, transform.position, CoinsManager.Instance.canvasRect), 1);
                CoinsManager.Instance.AddCoins(1);
                characterController.collectionParticle.Play();

                if (spider)
                {
                    var this_spider = Instantiate(this.spider, this.transform.position, this.spider.transform.rotation);
                    Destroy(this_spider, 0.5f);
                }

                SoundManager.Instance.PlayOneShot(SoundManager.Instance.eatInsect, 1);

                transform.DOLocalJump(new Vector3(UnityEngine.Random.Range(3, -3), UnityEngine.Random.Range(6, 11), UnityEngine.Random.Range(-2, -5)), 4, 1, .7f).OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });

                characterController.currentInsect.GetComponent<DOTweenAnimation>().DORestartById("Pop");
                x1Point.GetComponent<DOTweenAnimation>().DORestartById("Pop");
                x2Point.GetComponent<DOTweenAnimation>().DORestartById("Pop");

                if (Conff)
                    Conff.Play();

                float destroyTime = 1f;
                Invoke("DestroyConffety", destroyTime);
            }
            else
            {
                if (!isLizardScene)
                {
                    x1Point.GetComponent<DOTweenAnimation>().DORestartById("Pop");
                    x2Point.GetComponent<DOTweenAnimation>().DORestartById("Pop");
                    characterController.hitOnHigerlevelParticle.Play();
                    GameController.changeGameState(GameState.Complete);
                    StartCoroutine(Delay());
                    characterController.currentInsect.GetComponent<DOTweenAnimation>().DORestartById("Hit");
                }
            }
        }

        void EndPointFallEffect()
        {
            Rigidbody rb = endPointPart.GetComponent<Rigidbody>();

            rb.useGravity = true;
            rb.drag = 0f;
            rb.angularDrag = 0f;
            rb.AddForce(Vector3.down * fallForce, ForceMode.Impulse);

            Destroy(endPointPart, 2f);
        }

        void DestroyConffety()
        {
            if (Conff)
                Conff.Stop();

            if (Conff)
                Destroy(Conff.gameObject);
        }

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(0.4f);
            characterController.currentInsect.SetActive(false);
        }

        public void GroundAction()
        {
            if (levelCount <= characterController.currentLevelCount)
            {
                if (characterController.openWorldCheck)
                {
                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.eatInsect, 1);


                    if (gameObject.GetComponent<InsectID>())
                    {
                        gameObject.GetComponent<InsectID>().SelectInsect();
                        ReferenceManager.instance.characterController.id = gameObject.GetComponent<InsectID>().insectID;
                        FlagIdManager.Instance.DisableFlag(ReferenceManager.instance.characterController.id);
                        gameObject.GetComponentInParent<LookAtManager>().ObjectTurnedOff();
                        ArrowDetector.instance.gameObject.SetActive(false);
                        //   Debug.Log("2");
                    }


                    gameObject.SetActive(false);
                    //   Debug.Log("1");

                    if (gameObject.GetComponent<MoveTowardPlayer>())
                        gameObject.GetComponent<MoveTowardPlayer>().enabled = false;

                    if (spider)
                    {
                        var this_spider = Instantiate(this.spider, this.transform.position, this.spider.transform.rotation);
                        Destroy(this_spider, 1.5f);
                    }
                }
                else
                {
                    ReferenceManager.instance.characterController.currentInsectAnimator.SetTrigger("Eat");

                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.eatInsect, 1);
                    characterController.collectionParticle.Play();
                    DOVirtual.DelayedCall(0.4f, () => ReferenceManager.instance.characterController.currentInsectAnimator.SetTrigger("Walk"));

                    if (!isLizardScene)
                    {
                        characterController.CurrentPorgression(level, 0.25f);
                    }

                    gameObject.SetActive(false);
                    // Debug.Log("3");

                    characterController.currentInsect.GetComponent<DOTweenAnimation>().DORestartById("Pop");

                    if (spider)
                    {
                        var this_spider = Instantiate(this.spider, this.transform.position, this.spider.transform.rotation);
                        Destroy(this_spider, 1.5f);
                    }
                }
            }
            else
            {
                if (!isLizardScene && !characterController.openWorldCheck)
                {
                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.hitBiggerInsect, 1);
                    characterController.CurrentPorgression(level, -0.25f);
                    characterController.currentInsect.GetComponent<DOTweenAnimation>().DORestartById("Hit");
                }
                if (characterController.openWorldCheck)
                {
                    ReferenceManager.instance.characterController.checkOpenWorldCompleteState = true;
                    ReferenceManager.instance.uIManager.progressionBar.fillAmount = 0;
                    characterController.currentLevelCount = 0;
                    characterController.CurrentPorgression(level, 0f);
                }
            }
        }

        void UpdateTextColor(int level)
        {
            if (levelCount <= level)
            {
                if (levelText) levelText.color = Color.green;
                if (levelNumberText) levelNumberText.color = Color.green;
            }
            else
            {
                if (levelText) levelText.color = Color.red;
                if (levelNumberText) levelNumberText.color = Color.red;
            }
        }

        public enum CollectionType
        {
            onGround,
            onEndPoint
        }
    }
}