using UnityEngine;
using DG.Tweening;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace THEBADDEST.InteractSyetem
{
    public class Fight : TriggerEffector
    {
        public static Fight Instance;
        THEBADDEST.CharacterController3.CharacterController characterController;
        [SerializeField] Transform target;
        public Animator venom;
        [SerializeField] Transform targetLarwa;
        [SerializeField] GameObject venomPrefeb;
        [SerializeField] Transform venomInstantiatePos;
        [SerializeField] Transform parent;
        public GameObject endPointCharacter;
        public GameObject RemainendPointCharacter;
        public string activeSceneName;
        public GameObject healthBar;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            // Get the name of the active scene
            activeSceneName = SceneManager.GetActiveScene().name;

            if (healthBar) healthBar.SetActive(false);

            //// Output the results to the console
            //Debug.Log("Active Scene Index: " + activeSceneIndex);
            //Debug.Log("Active Scene Name: " + activeSceneName);

            UpdateEndPointCharacter();
        }

        protected override void OnEffect(Collider other, IEffectContainer container)
        {
            //triggered = true;
            base.OnEffect(other, container);
            if (other.CompareTag("Player"))
            {
                //venomDami.SetActive(false);

                // Check if the active scene has a specific name
                if (activeSceneName == "InsectScene")
                {
                    UpdateEndPoint = 1;
                    // Do something specific for this scene
                    //Debug.Log("This is the InsectScene scene.");
                }

                var venomClone = Instantiate(venomPrefeb, venomInstantiatePos.position, venomInstantiatePos.rotation);
                venomClone.transform.parent = parent;
                venom = venomClone.GetComponentInChildren<Animator>();
                if (healthBar) healthBar.SetActive(true);
                characterController = other.GetComponent<THEBADDEST.CharacterController3.CharacterController>();
                characterController.canvasPlayer.SetActive(false);
                Destroy(characterController.m_Rigidbody);
                characterController.GenrateLarwa(22, targetLarwa);
                characterController.upGradelevelParticle.Play();
                characterController.splineMove.ChangeSpeed(0);
                characterController.progressionbar.SetActive(false);
                characterController.behaviours[0].CanControl = false;
                characterController.behaviours[1].targetBoss = target;
                characterController.behaviours[1].CanControl = true;
                characterController.currentBehaviour++;
                characterController.currentInsectAnimator.SetTrigger("Idle");
                characterController.hand.SetActive(true);
                characterController.tap.SetActive(true);
                StartCoroutine(HideTutorial());
                venom.SetTrigger("Idle");
            }
        }

        IEnumerator HideTutorial()
        {
            yield return new WaitForSeconds(1);
            venom.SetTrigger("Idle");
            yield return new WaitForSeconds(6);
            characterController.hand.SetActive(false);
            characterController.tap.SetActive(false);
        }

        int UpdateEndPoint
        {
            set { PlayerPrefs.SetInt("UpdateEndPoint", value); }
            get { return PlayerPrefs.GetInt("UpdateEndPoint"); }
        }

        void UpdateEndPointCharacter()
        {
            if (UpdateEndPoint != 1)
            {
                if (endPointCharacter)
                    endPointCharacter.SetActive(true);
                if (RemainendPointCharacter)
                    RemainendPointCharacter.SetActive(false);
            }
            else
            {
                if (RemainendPointCharacter)
                    RemainendPointCharacter.SetActive(true);
                if (endPointCharacter)
                    endPointCharacter.SetActive(false);
            }
        }
    }
}