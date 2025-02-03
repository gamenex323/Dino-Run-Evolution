
using UnityEngine;
using DG.Tweening;
using GameAssets.GameSet.GameDevUtils.Managers;

namespace THEBADDEST.InteractSyetem
{
    public class Skull : TriggerEffector
    {
        THEBADDEST.CharacterController3.CharacterController characterController;

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Legs"))
            {
                ReferenceManager.instance.characterController.totallCollection++;
                Destroy(this.GetComponent<Collider>());
                transform.parent = other.transform;
                transform.localPosition = Vector3.zero;
                other.GetComponent<Collider>().enabled = false;
                this.transform.DOScale(0f, 0.5f).OnComplete(() =>
                {
                    Vibration.VibrateNope();
                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.skullcollection, 1);
                    other.GetComponent<Collider>().enabled = true;
                    SkullCOunt++;
                    if (SkullCOunt == 3)
                    {
                        SkullCOunt = 0;
                        CoinsManager.Instance.AddCoins(1);
                    }
                    CoinsManager.Instance.AddCoins(CoinsManager.Instance.WordPointToCanvasPoint(CoinsManager.Instance.mainCam, transform.position, CoinsManager.Instance.canvasRect), 1);
                    Vibration.VibrateNope();
                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.skullcollection, 1);
                    ReferenceManager.instance.characterController.skullPickupParticle.Play();
                    Destroy(this.gameObject);
                });
            }

            if (other.CompareTag("Player"))
            {
                // Debug.Log("40");
                characterController = other.GetComponent<THEBADDEST.CharacterController3.CharacterController>();

                if (characterController.openWorldCheck)
                {
                    gameObject.SetActive(false);
                    characterController.totallCollection++;
                    CoinsManager.Instance.AddCoins(CoinsManager.Instance.WordPointToCanvasPoint(CoinsManager.Instance.mainCam, transform.position, CoinsManager.Instance.canvasRect), 1);
                    CoinsManager.Instance.AddCoins(1);
                    //Vibration.VibrateNope();

                    ReferenceManager.instance.characterController.totallCollection_Text.text = characterController.totallCollection.ToString();
                    ReferenceManager.instance.characterController.currentsquidManseatValue += 1;
                    ReferenceManager.instance.characterController.eatTotalSquidMans_Text.text = ReferenceManager.instance.characterController.
                    currentsquidManseatValue.ToString();
                    //  Debug.Log("4");
                }
                else
                {
                    gameObject.SetActive(false);
                    characterController.skullPickupParticle.Play();
                    characterController.totallCollection++;
                    CoinsManager.Instance.AddCoins(CoinsManager.Instance.WordPointToCanvasPoint(CoinsManager.Instance.mainCam, transform.position, CoinsManager.Instance.canvasRect), 1);
                    CoinsManager.Instance.AddCoins(1);
                    Vibration.VibrateNope();
                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.skullcollection, 1);
                    //  Debug.Log("5");
                }
            }
        }

        int SkullCOunt
        {
            get { return PlayerPrefs.GetInt("SkullCOunt"); }
            set { PlayerPrefs.SetInt("SkullCOunt", value); }
        }
    }
}