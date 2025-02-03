using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOpenWorldCharacter : MonoBehaviour
{
    public GameObject character_G;

    private void Start()
    {
        character_G.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            character_G.SetActive(true);
        }
    }
}
