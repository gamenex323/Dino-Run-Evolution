using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THEBADDEST.CharacterController3
{
    public class PlayerInvisible : MonoBehaviour
    {
        private CharacterController characterInvisible;
        //public GameObject playerInvisible;

        private void Start()
        {
            characterInvisible = FindObjectOfType<CharacterController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //_Sajid_Saingal   AdController.instance?.ShowRewarded(TriggerInvisibleCharacter);
            }
        }

        public void TriggerInvisibleCharacter()
        {
            TriggerCheck[] triggerCheck = characterInvisible.insect.GetComponentsInChildren<TriggerCheck>();

            for (int i = 0; i < triggerCheck.Length; i++)
            {
                triggerCheck[i].gameObject.GetComponent<SphereCollider>().enabled = false;
            }

            StartCoroutine(TriggerVisibleCharacter());
        }

        private IEnumerator TriggerVisibleCharacter()
        {
            yield return new WaitForSeconds(10f);

            TriggerCheck[] triggerCheck = characterInvisible.insect.GetComponentsInChildren<TriggerCheck>();

            for (int i = 0; i < triggerCheck.Length; i++)
            {
                triggerCheck[i].gameObject.GetComponent<SphereCollider>().enabled = true;
            }
        }

    }
}
