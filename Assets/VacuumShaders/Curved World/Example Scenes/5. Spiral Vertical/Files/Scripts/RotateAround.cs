﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VacuumShaders.CurvedWorld.Example
{
    public class RotateAround : MonoBehaviour
    {
        public Vector3 eular;

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(eular * Time.deltaTime * 12);
        }
    }
}