using GameDevUtils.HealthSystem;
using THEBADDEST;
using THEBADDEST.InteractSyetem;
using UnityEngine;
using GameDevUtils;
using SWS;
using DG.Tweening;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.UI;
using MergeSystem;
using GalarySystem;
using Sirenix.OdinInspector;
using Cinemachine;
using UnityEngine.SceneManagement;
using GameAssets.GameSet.GameDevUtils.Managers;
using TMPro;
using System.Net;
using UnityEngine.UIElements;
using UnityEngine.AI;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace THEBADDEST.CharacterController3
{
    public class CharacterController : GameDevBehaviour, IEffectContainer, IDamageable
    {
        public Rigidbody m_Rigidbody;
        [SerializeField] bool isTesting;
        [SerializeField] int testLevelCount;
        [SerializeField] Transform spownPos;
        public ParticleSystem win;
        public DOTweenAnimation barSprite;
        public Material greyInsect;
        public GameObject hand;
        public GameObject tap;
        bool inGamePlay;
        public GameObject prefebShoot;
        [Header("---------Collection Particles--------")]
        public ParticleSystem lizardCollection;
        public ParticleSystem collectionParticle;
        [FoldoutGroup("Particles")]
        public ParticleSystem deathParticle;
        public GameObject splashPrefeb;
        [FoldoutGroup("Particles")]
        public ParticleSystem skullPickupParticle;
        [FoldoutGroup("Particles")]
        public ParticleSystem hitOnHigerlevelParticle;
        [FoldoutGroup("Particles")]
        public ParticleSystem upGradelevelParticle;
        [Header("---------Player Information--------")]
        public GameObject canvasPlayer;
        public GameObject progressionbar;
        public GameObject levelFiller;
        public List<InsectsData> insects = new List<InsectsData>();
        public GameObject currentInsect;
        public Animator currentInsectAnimator;
        public LevelType currentLevel;
        public Text upGradeText;
        public int currentLevelCount;
        [Header("---------Player Behaviours--------")]
        [SerializeField] public CharacterBehaviour[] behaviours;
        [SerializeField] HealthSystem healthSystem;
        public int currentBehaviour = 0;
        IEffectContainer effectContainer;
        public splineMove splineMove;
        float splineSpeed;

        public UIManager uIManager;
        public UnityEngine.UI.Image reviveFiller;
        public bool IsDestroyed { get; set; }
        float progressionValue;
        public int totallCollection;
        [Header("---------Camera Animator--------")]
        public GameObject camera;
        public Animator cinemachine;
        [Header("---------End Point Larwa--------")]
        [SerializeField] GameObject larwaPrefeb;
        public int totalLarwa;
        public int eatLarwa;
        public static event Action<bool> onFight;
        public static event Action<int> onCharacterUpdate;
        int specialCharacterId;
        public DOTweenAnimation PopAnimation;
        public CinemachineVirtualCamera virtualCamera;

        [Header("---------Open World--------")]
        public GameObject openWorldEnv;
        public Transform openWorldInsectJumpSpawnPos;
        public Transform openWorldInsectSpawnPos;
        public int openWorldInsectSpawnPos_Index;
        public GameObject endPoint;
        public GameObject openWorldFadePanel;
        public GameObject cinemachineAnimator;
        public FloatingJoystick insectJoystick;
        public InsectController insectController;
        public UnityEngine.UI.Button openWorldBackButton;
        public ParticleSystem portalParticle;
        ParticleSystem portal;
        public Transform portalParticleSpawnPoint;
        public GameObject lineRendererPrefeb;
        public GameObject lineRendererPrefeb_1;
        public Transform lineTarget;
        public ParticleSystem squidManBoneParticle;
        public Transform squidManBoneParticleSpawnPoint;
        public bool openWorldCheck;
        public bool openWorldEatableCheck;
        public Text eatTotalSquidMans_Text;
        public Text totallCollection_Text;
        internal int currentsquidManseatValue;
        public Transform ropeStartPoint;
        public GameObject portalParticles;
        public GameObject areaWinParticle;
        public GameObject areaWinParticleSpawnPoint;
        public Transform insect;
        public int id;
        public Transform cameraTransform;
        public CinemachineVirtualCamera virtualCameraOpenWorld;
        public CinemachineVirtualCamera cutSceneCamera;
        public CinemachineVirtualCamera casleCamera;
        public GameObject offAllAi;
        public GameObject arrow;
        public MoveTowardPlayer[] moveScript;
        public LookAtManager LookAtManager;
        public bool checkOpenWorldCompleteState = false;
        public GameObject openWorldCharacters;
        public ParticleSystem detectAreaParticleSystem;
        public GameObject cutSceneCasle;
        public UpgradeCharOpenWorld panelsToDeactivate;

        public Transform playerBoostpar_SpawnPoint;
        public ParticleSystem playerBoostpar;

        public Transform freePlayEnv;
        public Transform freePlaySpawnPoint;

        public TextMeshProUGUI enemyHealthBar_Text;
        public UnityEngine.UI.Image showLizardHealthBarOnPanel;

        void Awake()
        {

#if UNITY_EDITOR
            Debug.unityLogger.logEnabled = true;
#else
                                Debug.unityLogger.logEnabled = false;
#endif

            InitializeAllBehaviors();

            if (cinemachineAnimator)
                cinemachineAnimator.SetActive(false);

            effectContainer = new EffectContainer();
            GameController.onGameplay += GameStart;
            GameController.onLevelFail += GameStop;
            GameController.onCompleteOpenWorldLevel += GameStop;
            GameController.OnRevive += GameStop;

            if (ReferenceManager.instance.tutorialManager) ReferenceManager.instance.tutorialManager.OnTutorial += GamepauseOnTutorial;

            GameController.onLevelComplete += GameStop;

            if (isTesting)
            {
                currentLevelCount = testLevelCount;
            }
            else
            {
                if (PlayerPrefs.GetInt("LevelOfChara" + PlayerPrefManager.GetSceneName()) == 0)
                {
                    PlayerPrefs.SetInt("LevelOfChara" + PlayerPrefManager.GetSceneName(), currentLevelCount);
                    currentLevelCount = PlayerPrefs.GetInt("LevelOfChara" + PlayerPrefManager.GetSceneName());
                }
                else
                {
                    currentLevelCount = PlayerPrefs.GetInt("LevelOfChara" + PlayerPrefManager.GetSceneName());
                }
            }

            GalaryManager.onSpecialItemUpdate += SetSpecialItem;
        }

        public void RewardedYesClick()
        {
            //_Sajid_Saingal  AdController.instance?.ShowRewarded(panelsToDeactivate.UpgradeCharacterOpenWorld);
        }

        public void RewardedNoClick()
        {
            if (panelsToDeactivate.upgradeCharPanel)
                panelsToDeactivate.upgradeCharPanel.SetActive(false);

            if (insectController)
                insectController.insectMoveSpeed = 8;

            if (insectController.insectJoystick)
                insectController.insectJoystick.gameObject.SetActive(true);

            if (panelsToDeactivate.rewardRange)
                panelsToDeactivate.rewardRange.SetActive(false);
        }

        public void OnClickRestart()
        {
            SceneManager.LoadScene(1);
        }

        protected override void Start()
        {
            PlayerPrefs.GetInt("OpenWorldTutorial", 1);

            if (arrow)
                arrow.SetActive(false);

            if (eatTotalSquidMans_Text)
                eatTotalSquidMans_Text.text = currentsquidManseatValue.ToString();

            if (openWorldEnv)
                openWorldEnv.SetActive(false);
            if (openWorldFadePanel)
                openWorldFadePanel.gameObject.SetActive(false);
            if (insectJoystick)
                insectJoystick.gameObject.SetActive(false);
            if (insectController)
                insectController.enabled = false;
            if (openWorldBackButton)
                openWorldBackButton.gameObject.SetActive(false);
            if (openWorldCheck)
                openWorldCheck = false;
            if (openWorldEatableCheck)
                openWorldEatableCheck = false;


            base.Start();
            RunBehaviour(0);
            currentLevel = (LevelType)currentLevelCount;
            UpgardeLevelOfCharacter(currentLevel);


            if (!ReferenceManager.instance.uIManager.isLizardScene)
            {
                InstantiateFreeplay();
            }

        }

        void InstantiateFreeplay()
        {
            Transform instantiatedFreePlayEnv = Instantiate(freePlayEnv, freePlayEnv.position, freePlayEnv.rotation);
            instantiatedFreePlayEnv.SetParent(freePlaySpawnPoint);

            instantiatedFreePlayEnv.localPosition = Vector3.zero;
            instantiatedFreePlayEnv.localRotation = Quaternion.identity;

            cutSceneCasle = instantiatedFreePlayEnv.GetComponentInChildren<FindFreePlay>().gameObject;
            if (cutSceneCasle) cutSceneCasle.SetActive(true);
        }

        GameObject portal1;

        public void OpenWorldScenario()
        {
            ReferenceManager.instance.uIManager.competePanel.SetActive(false);
            ReferenceManager.instance.levelManager.currentLevel.levelData.gameObject.SetActive(false);
            openWorldBackButton.gameObject.SetActive(true);
            currentInsect.gameObject.SetActive(true);
            currentInsectAnimator.SetTrigger("Walk");
            Fight.Instance.endPointCharacter.SetActive(false);
            Fight.Instance.RemainendPointCharacter.SetActive(false);

            SoundManager.Instance.PlayOneShot(SoundManager.Instance.portalOpenSound, 1);
            portal1 = Instantiate(portalParticles, portalParticleSpawnPoint.transform.position, portalParticleSpawnPoint.transform.rotation);

            this.transform.DOMoveZ(this.transform.position.z + 3f, 2f, false).SetEase(Ease.Linear).OnComplete(delegate
            {
                portal1.gameObject.SetActive(false);

                float DestoyPortalPar = 1f;
                Invoke("DestroyPortalPartoicle", DestoyPortalPar);

                insectController.enabled = true;
                openWorldFadePanel.SetActive(true);

                openWorldCharacters.SetActive(false);
                offAllAi.SetActive(false);

                Camera.main.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = 0;

                if (CasleCutScene == 0)
                {
                    casleCamera.gameObject.SetActive(true);
                    // CasleCutScene = 1;
                }


                DOVirtual.DelayedCall(0.3f, () => openWorldFadePanel.SetActive(false)).OnComplete(delegate
                {
                    Camera.main.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = 2;
                    DOVirtual.DelayedCall(1f, () => casleCamera.gameObject.SetActive(false));


                    DOVirtual.DelayedCall(8f, () => Camera.main.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = 1);

                    if (PlayerPrefs.GetInt("OpenWorldJoyStickTutorial") == 0)
                    {
                        DOVirtual.DelayedCall(3.5f, () => ReferenceManager.instance.uIManager.homeOpenWorldPanel.SetActive(true));
                        PlayerPrefs.SetInt("OpenWorldJoyStickTutorial", 1);
                    }

                    if (CasleCutScene == 0)
                    {
                        FirstTime_Freeplay_Assets();
                    }
                    if (CasleCutScene == 1)
                    {
                        OtherTime_Freeplay_Assets();
                    }

                    InsectIdManager.instance.SetNumber();

                    if (arrow)
                        arrow.SetActive(true);

                    currentInsect.gameObject.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 0.01f);



                    cinemachine.enabled = false;
                    cinemachineAnimator.SetActive(false);

                    virtualCameraOpenWorld.transform.SetAsFirstSibling();

                    insectJoystick.gameObject.SetActive(true);

                    if (openWorldEnv)
                        openWorldEnv.SetActive(true);

                    endPoint.SetActive(false);
                    this.transform.position = openWorldInsectSpawnPos.position;

                    currentInsect.gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
                    currentInsect.gameObject.transform.GetChild(1).GetChild(0).gameObject.transform.localScale = Vector3.zero;


                    DOVirtual.DelayedCall(2, delegate
                    {
                        PlayerDetectionArea();
                    });

                    openWorldCheck = true;

                    if (openWorldCheck)
                    {
                        canvasPlayer.gameObject.SetActive(true);
                        canvasPlayer.gameObject.transform.DOScale(new Vector3(-0.02f, -0.02f, -0.02f), 0.0001f);

                        for (int i = 0; i < insects.Count; i++)
                        {
                            canvasPlayer.gameObject.transform.DOMoveY(insects[i].canvusOffset + 2f, 0.01f);
                        }

                        MoveTowardPlayer[] moveTowardPlayers = insect.GetComponentsInChildren<MoveTowardPlayer>();

                        for (int i = 0; i < moveTowardPlayers.Length; i++)
                        {
                            moveTowardPlayers[i].target = currentInsect.transform;
                        }
                    }
                });
            });
        }

        void FirstTime_Freeplay_Assets()
        {
            DOVirtual.DelayedCall(3.5f, () => offAllAi.SetActive(true));
            DOVirtual.DelayedCall(3.5f, () => openWorldCharacters.SetActive(true));
            DOVirtual.DelayedCall(3f, () => cutSceneCasle.SetActive(false));
            DOVirtual.DelayedCall(3.5f, () => CasleCutScene = 1);
        }

        void OtherTime_Freeplay_Assets()
        {
            offAllAi.SetActive(true);
            openWorldCharacters.SetActive(true);
            cutSceneCasle.SetActive(false);
        }

        void DestroyPortalPartoicle()
        {
            Destroy(portal1.gameObject);
        }

        public void PlayerDetectionArea()
        {
            detectAreaParticleSystem = currentInsect.GetComponentInChildren<ParticleSystem>();
            // Debug.Log("AreaParticle :" + detectAreaParticleSystem);

            detectAreaParticleSystem.Play();
            // Debug.Log("AreaParticle_Play :" + detectAreaParticleSystem);

            {
                float SizeVariable = 0.016f;

                detectAreaParticleSystem.gameObject.transform.parent.localScale = Vector3.one;

                detectAreaParticleSystem.gameObject.transform.localScale = Vector3.one * (.7f);

                int number = (int)currentLevel;

                if (number > 25)
                {
                    number = 25;
                }

                detectAreaParticleSystem.gameObject.transform.localScale += Vector3.one * (number * SizeVariable);
                LookAtManager.GetColliders(detectAreaParticleSystem.gameObject.transform.localScale);
            }
        }

        private void InitializeAllBehaviors()
        {
            foreach (CharacterBehaviour behaviour in behaviours)
            {
                behaviour.Init(this);
            }
        }

        public void StopAllBehaviours()
        {
            foreach (CharacterBehaviour behaviour in behaviours)
            {
                behaviour.CanControl = false;
            }
        }

        public void StartOnRewardedPathClampBehaviours()
        {
            foreach (CharacterBehaviour behaviour in behaviours)
            {
                behaviour.clampValueMax = 0.5f;
                behaviour.clampValueMin = -0.5f;
                splineMove.speed = UpgardeManager.instace.speed + 2;
            }
        }

        public void StartOnNormalPathClampBehaviours()
        {
            foreach (CharacterBehaviour behaviour in behaviours)
            {
                behaviour.clampValueMax = 1.8f;
                behaviour.clampValueMin = -2.5f;
                splineMove.speed = UpgardeManager.instace.speed;
            }
        }

        public void RunBehaviour(int behaviorIndex)
        {
            behaviours[currentBehaviour].CanControl = false;
            currentBehaviour = behaviorIndex;
            behaviours[behaviorIndex].CanControl = false;
        }

        public void RunAdditiveBehaviour(int behaviorIndex)
        {
            behaviours[behaviorIndex].CanControl = true;
        }

        public void AddEffect(string id, IEffect effect)
        {
            effectContainer.AddEffect(id, effect);
        }

        public void EmitEffect(IEffector effect)
        {
            effectContainer.EmitEffect(effect);
        }

        public void EmitEffect(string effectId)
        {
            effectContainer.EmitEffect(effectId);
        }

        public void Damage(float damageAmount, Vector3 hitPoint)
        {
            healthSystem.TakeDamage(damageAmount, hitPoint);
        }

        public void DestroyObject()
        {
            healthSystem.Death();
        }

        //SetItemOnCharacter

        void SetSpecialItem(int id)
        {

            //if (GalaryManager.SpecialItemUnlock == 1)
            //{

            if (!currentInsect) return;
            var specialItem = currentInsect.GetComponentInChildren<SpecialItem>();
            if (specialItem)
            {
                specialItem.SetItemOnCHaracter(id);
            }
            //}
        }

        void GameStart()
        {
            behaviours[currentBehaviour].CanControl = true;
            splineMove.speed = PlayerPrefs.GetFloat("Speed" + PlayerPrefManager.GetSceneName());
            splineMove.StartMove();
            currentInsectAnimator.SetTrigger("Walk");
            inGamePlay = true;
        }

        void GameStop()
        {
            canvasPlayer.SetActive(false);
            behaviours[currentBehaviour].CanControl = false;
            splineMove.Pause();
            //m_Rigidbody.isKinematic = true;
        }
        void GamepauseOnTutorial(bool canStop)
        {

            canvasPlayer.SetActive(!canStop);
            behaviours[currentBehaviour].CanControl = !canStop;
            if (canStop)
            {
                splineSpeed = splineMove.speed;
                splineMove.speed = 0;
                splineMove.Stop();
            }
            else
            {
                //inGamePlay = true;
                //splineMove.speed = PlayerPrefs.GetFloat("Speed" + PlayerPrefManager.GetSceneName());
                //splineMove.StartMove();
                //behaviours[currentBehaviour].CanControl = true;
                //Debug.Log(spineSpeed);
            }
        }

        public void CurrentPorgression(LevelType levelType, float value)
        {
            UpdateBar(value);
        }

        public void UpdateBar(float value)
        {
            var temp = uIManager.progressionBar.fillAmount;
            temp += value;

            //Debug.Log("Temp :" + temp);

            uIManager.progressionBar.DOFillAmount(temp, .1f).OnComplete(() =>
            {
                OnCompleteCheckProgression();
            });

            if (barSprite) barSprite.DORestartById("Rotate");
        }

        void OnCompleteCheckProgression()
        {
            if (uIManager.progressionBar.fillAmount >= 1)
            {
                currentLevelCount++;
                currentLevel = (LevelType)currentLevelCount;
                UpgardeLevelOfCharacter(currentLevel);
                uIManager.progressionBar.fillAmount = 0;
            }
            else if (uIManager.progressionBar.fillAmount <= 0 && currentLevelCount > 0)
            {
                currentLevelCount--;

                if (currentLevelCount != 0)
                {
                    currentLevel = (LevelType)currentLevelCount;
                    UpgardeLevelOfCharacter(currentLevel);

                    if (currentLevelCount != 0)
                    {
                        uIManager.progressionBar.fillAmount = .99f;
                    }
                    else
                    {
                        uIManager.progressionBar.fillAmount = 0;
                    }
                }
                else
                {
                    GameController.changeGameState(GameState.Fail);
                    virtualCamera.enabled = false;
                    currentInsect.transform.DOShakeRotation(0.15f, new Vector3(300f, 300f, 300f)).SetEase(Ease.Linear).OnComplete(delegate
                    {
                        currentInsect.transform.DORotate(new Vector3(0, 180f, 0), 0.3f).OnComplete(delegate
                        {
                            currentInsect.transform.DOScale(Vector3.zero, 0.35f).OnComplete(delegate
                            {
                                currentInsect.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.001f);
                                deathParticle.Play();
                                hitOnHigerlevelParticle.Play();
                                currentInsect.SetActive(false);
                            });
                        });
                    });
                }
            }
            else if (uIManager.progressionBar.fillAmount <= 0 && currentLevelCount > 0)
            {
                GameController.changeGameState(GameState.Fail);
                virtualCamera.enabled = false;
                currentInsect.transform.DOShakeRotation(0.15f, new Vector3(300f, 300f, 300f)).SetEase(Ease.Linear).OnComplete(delegate
                {
                    currentInsect.transform.DORotate(new Vector3(0, 180f, 0), 0.3f).OnComplete(delegate
                    {
                        currentInsect.transform.DOScale(Vector3.zero, 0.35f).OnComplete(delegate
                        {
                            currentInsect.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.001f);
                            deathParticle.Play();
                            hitOnHigerlevelParticle.Play();
                            currentInsect.SetActive(false);
                        });
                    });
                });
            }
            else if (uIManager.progressionBar.fillAmount <= 0 && currentLevelCount == 0 && checkOpenWorldCompleteState)
            {
                if (openWorldCheck)
                {
                    insectJoystick.gameObject.SetActive(false);
                    insectController.enabled = false;
                    GameController.changeGameState(GameState.CompleteOpenWorld);

                    virtualCamera.enabled = false;
                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.bombHitClip, 1);
                    currentInsect.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.001f);
                    deathParticle.Play();
                    hitOnHigerlevelParticle.Play();
                    currentInsect.SetActive(false);
                    openWorldCheck = false;
                    checkOpenWorldCompleteState = false;
                }
            }
        }

        public void UpgardeLevelOfCharacter(LevelType levelType)
        {
            currentLevel = levelType;
            ChangeCharacter(levelType);
            if (currentLevelCount <= 9)
            {
                upGradeText.text = "0" + currentLevelCount.ToString();
            }
            else
            {
                upGradeText.text = currentLevelCount.ToString();
            }
            SetSpecialItem(GalaryManager.SpecialItemId);
        }

        void UpdatePlayerCanvas(float val)
        {
            var canvas = canvasPlayer.transform.localPosition;
            canvas.y = val;
            canvasPlayer.transform.localPosition = canvas;
        }

        void ChangeCharacter(LevelType level)
        {
            //upGradelevelParticle.Play();
            if (level <= LevelType.Level35)
            {
                onCharacterUpdate?.Invoke((int)level);

                for (int i = 0; i < insects.Count; i++)
                {
                    if (insects[i].level == level)
                    {
                        insects[i].insect.SetActive(true);

                        currentInsect = insects[i].insect;

                        currentInsectAnimator = insects[i].animator;

                        if (inGamePlay)
                        {
                            insects[i].animator.SetTrigger("Walk");
                        }

                        if (this.transform.GetChild(0).TryGetComponent<DOTweenAnimation>(out DOTweenAnimation animation))
                        {
                            animation.DORestartById("Rotate");
                        }

                        UpdatePlayerCanvas(insects[i].canvusOffset);
                    }
                    else
                    {
                        insects[i].insect.SetActive(false);
                    }
                }
            }
        }

        public void TestCharacter()
        {
            currentLevelCount++;
            PlayerPrefs.SetInt("LevelOfChara" + PlayerPrefManager.GetSceneName(), currentLevelCount);
            ChangeCharacter((LevelType)currentLevelCount);
        }

        public void CreateEnemiesAroundPoint(int num, Vector3 point, float radius, Transform target)
        {
            var required = new Vector3(0, -23, 0);
            this.transform.GetChild(0).transform.DORotate(required, 1);
            this.transform.GetChild(0).transform.DOScale(0.5f, 0.5f).SetRelative();
            StartCoroutine(Delay(num, point, radius, target));
        }

        IEnumerator Delay(int num, Vector3 point, float radius, Transform target)
        {
            for (int i = 0; i < num; i++)
            {
                yield return new WaitForSeconds(0.05f);
                /* Distance around the circle */
                var radians = 2 * Mathf.PI / num * i;

                /* Get the vector direction */
                var vertical = Mathf.Sin(radians);
                var horizontal = Mathf.Cos(radians);

                var spawnDir = new Vector3(horizontal, 0, vertical);

                /* Get the spawn position */
                var spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

                /* Now spawn */
                var enemy = Instantiate(larwaPrefeb, this.transform.position, Quaternion.identity) as GameObject;

                /* Rotate the enemy to face towards player */
                enemy.transform.LookAt(point);
                enemy.transform.localScale = Vector3.zero;
                enemy.transform.position = spawnPos;
                /* Adjust height */
                //enemy.transform.Translate(new Vector3(0, 0.8f, 0));
                //enemy.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
                //enemy.transform.DOJump(spawnPos, 4, 1, .3f);
                //var errorValue = new Vector3(spawnPos.x + UnityEngine.Random.Range(4f, 6f), spawnPos.y, spawnPos.z + UnityEngine.Random.Range(6f, 9f));
                // enemy.transform.DOMove(spawnPos, .5f).SetEase(Ease.OutBack).OnComplete(() => { enemy.transform.DOJump(this.transform.position, 7, 1, .3f); });
                enemy.transform.DOScale(1, 0.3f).SetEase(Ease.OutBounce).OnComplete(() =>
                {
                    enemy.AddComponent<Rigidbody>();
                    enemy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    enemy.GetComponent<Lawra>().canMove = true;
                    enemy.GetComponent<Lawra>().target = target;
                    // Constraints Frezz kro
                });
            }

            FunctionTimer.Create(() => { onFight?.Invoke(true); }, 0.5f);
        }

        public void GenrateLarwa(int amount, Transform target)
        {
            totalLarwa = amount;
            CreateEnemiesAroundPoint(amount, this.transform.position, 5f, target);
        }

        public void Revive()
        {
            StartCoroutine(DelayInRevive());
        }

        IEnumerator DelayInRevive()
        {
            //_Sajid_Saingal  AdController.instance?.ShowRewarded(ReviveCharacter);
            yield return new WaitForSeconds(0.5f);
        }

        void ReviveCharacter()
        {
            GameController.changeGameState(GameState.Revive);

            //X  if (HomeScreen.instance) HomeScreen.instance.HideLargeBanner();
            //X if (HomeScreen.instance) HomeScreen.instance.ShowBanner();

            virtualCamera.enabled = true;
            this.transform.localPosition = new(-.6f, transform.localPosition.y, transform.localPosition.z + 5);
            currentInsect.SetActive(true);

            this.transform.DOScale(1, 0.01f);
            currentInsect.transform.DOScale(0, 0.01f);
            currentInsect.transform.DOScale(1, 1f).SetEase(Ease.OutBounce).SetRelative();

            reviveFiller.transform.parent.gameObject.SetActive(true);
            FunctionTimer.Create(() =>
            {
                splineMove.Resume();
                behaviours[0].CanControl = true;
                currentInsectAnimator.SetTrigger("Walk");
                reviveFiller.transform.parent.gameObject.SetActive(false);
                FunctionTimer.Create(() =>
                {
                    this.GetComponent<Collider>().enabled = true;
                    m_Rigidbody.isKinematic = false;
                }, 0.2f);
            }, reviveFiller, 2, antiClock: true);


            //_Sajid_Saingal  FirebaseConnection.instance?.Check_RewardedAdsOn_ReviveChar();
        }

        [SerializeField] public GameObject[] AnimateCashOnVRewardedVideo;
        [SerializeField] public Transform target;
        [SerializeField] public Transform cashInitialPos;

        public void DoubleReward()
        {
            //_Sajid_Saingal AdController.instance?.ShowRewarded(OpenWorldDoubleReward);
        }

        void OpenWorldDoubleReward()
        {
            AnimateCash();
            var openWorldCash = totallCollection *= 2;
            CoinsManager.Instance.AddCoins(openWorldCash);
            DOVirtual.DelayedCall(1f, () => SceneLoad());
        }

        void SceneLoad()
        {
            SceneManager.LoadScene(2);
        }

        public void AnimateCash()
        {
            for (int i = 0; i < AnimateCashOnVRewardedVideo.Length; i++)
            {
                AnimateCashOnVRewardedVideo[i].SetActive(true);

                for (int J = 0; J < AnimateCashOnVRewardedVideo.Length; J++)
                {
                    AnimateCashOnVRewardedVideo[0].transform.DOMove(target.transform.position, 0.15f).SetEase(Ease.OutBounce).OnComplete(delegate
                    {
                        AnimateCashOnVRewardedVideo[0].transform.gameObject.SetActive(false);
                        AnimateCashOnVRewardedVideo[0].transform.position = cashInitialPos.position;
                    }).SetUpdate(true);
                    AnimateCashOnVRewardedVideo[1].transform.DOMove(target.transform.position, 0.35f).SetEase(Ease.OutBounce).OnComplete(delegate
                    {
                        AnimateCashOnVRewardedVideo[1].transform.gameObject.SetActive(false);
                        AnimateCashOnVRewardedVideo[1].transform.position = cashInitialPos.position;
                    }).SetUpdate(true);
                    AnimateCashOnVRewardedVideo[2].transform.DOMove(target.transform.position, 0.65f).SetEase(Ease.OutBounce).OnComplete(delegate
                    {
                        AnimateCashOnVRewardedVideo[2].transform.gameObject.SetActive(false);
                        AnimateCashOnVRewardedVideo[2].transform.position = cashInitialPos.position;
                    }).SetUpdate(true);
                    AnimateCashOnVRewardedVideo[3].transform.DOMove(target.transform.position, 0.75f).SetEase(Ease.OutBounce).OnComplete(delegate
                    {
                        AnimateCashOnVRewardedVideo[3].transform.gameObject.SetActive(false);
                        AnimateCashOnVRewardedVideo[3].transform.position = cashInitialPos.position;
                    }).SetUpdate(true);
                }
            }
        }

        private void OnDestroy()
        {
            onFight = null;
        }

        private static string _CasleCutScene = "CasleCutScene";

        public static int CasleCutScene
        {
            get
            {
                return PlayerPrefs.GetInt(_CasleCutScene);
            }
            set
            {
                PlayerPrefs.SetInt(_CasleCutScene, value);
            }
        }

        [Serializable]
        public class InsectsData
        {
            [SerializeField] public GameObject insect;
            [SerializeField] public LevelType level;
            [SerializeField] public ParticleSystem transformationParticle;
            [SerializeField] public Animator animator;
            public float canvusOffset;
        }
    }
}

public enum LevelType
{
    Level1 = 1,
    Level2 = 2,
    Level3 = 3,
    Level4 = 4,
    Level5 = 5,
    Level6 = 6,
    Level7 = 7,
    Level8 = 8,
    Level9 = 9,
    Level10 = 10,
    Level11 = 11,
    Level12 = 12,
    Level13 = 13,
    Level14 = 14,
    Level15 = 15,
    Level16 = 16,
    Level17 = 17,
    Level18 = 18,
    Level19 = 19,
    Level20 = 20,
    Level21 = 21,
    Level22 = 22,
    Level23 = 23,
    Level24 = 24,
    Level25 = 25,
    Level26 = 26,
    Level27 = 27,
    Level28 = 28,
    Level29 = 29,
    Level30 = 30,
    Level31 = 31,
    Level32 = 32,
    Level33 = 33,
    Level34 = 34,
    Level35 = 35,
}