using GameAssets.GameSet.GameDevUtils.Managers;
using System.Collections;
using System.Collections.Generic;
using THEBADDEST.CharacterController3;
using Unity.VisualScripting;
using UnityEngine;

public class RaceSkull : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RacePlayer"))
        {
            if (other.gameObject.GetComponentInParent<RacePlayer>())
            {
                this.gameObject.SetActive(false);
                CoinsManager.Instance.AddCoins(CoinsManager.Instance.WordPointToCanvasPoint(CoinsManager.Instance.mainCam, transform.position, CoinsManager.Instance.canvasRect), 1);
                CoinsManager.Instance.AddCoins(1);
                Vibration.VibrateNope();
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.skullcollection, 1);
            }
        }
    }
}


