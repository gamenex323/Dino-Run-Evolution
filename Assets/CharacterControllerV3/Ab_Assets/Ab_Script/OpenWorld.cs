using System.Collections;
using System.Collections.Generic;
using THEBADDEST.CharacterController3;
using UnityEngine;

namespace THEBADDEST.InteractSyetem
{
    public class OpenWorld : MonoBehaviour
    {
        THEBADDEST.CharacterController3.CharacterController characterController;

        // Start is called before the first frame update
        void Start()
        {
            OpenWorldScene();
        }

        public void OpenWorldScene()
        {
            characterController.currentInsect.SetActive(true);
        }
    }
}
