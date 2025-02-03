using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using THEBADDEST.InteractSyetem;
using THEBADDEST.CharacterController3;
using TMPro;

namespace THEBADDEST.InteractSyetem
{
    public class Boss : MonoBehaviour
    {
        THEBADDEST.CharacterController3.CharacterController characterController;

        [SerializeField] Image healthBar;
        [SerializeField] Image lizardHealthBar;
        [SerializeField] bool canUpdate;
        [SerializeField] ParticleSystem particleSystem;
        [SerializeField] ParticleSystem hitParticle;
        [SerializeField] Transform jump;
        [SerializeField] Transform attackJump;
        public CharacterController3.CharacterController character;


        private void Awake()
        {
            character = FindObjectOfType<CharacterController3.CharacterController>();
        }

        private void Start()
        {
            float savedHealth = PlayerPrefs.GetFloat("CurrentHealtBarState", 1f);

            if (savedHealth == 0)
            {
                savedHealth = 1f;
                PlayerPrefs.SetFloat("CurrentHealtBarState", savedHealth);
                Debug.Log("Health bar was 0. Resetting to 1.");
            }

            if (lizardHealthBar) lizardHealthBar.fillAmount = savedHealth;
            if (character.showLizardHealthBarOnPanel) character.showLizardHealthBarOnPanel.fillAmount = savedHealth;
            Debug.Log($"Restored health bar state: {savedHealth}");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
            {
                if (Fight.Instance.activeSceneName == "InsectScene")
                {
                    UpdateBar(-0.15f);
                }

                if (Fight.Instance.activeSceneName == "LizardScene")
                {
                    UpdateBarInLizradScene(-0.035f);
                    Fight.Instance.venom.SetTrigger("Hit");
                    DOVirtual.DelayedCall(0.5f, () => Fight.Instance.venom.SetTrigger("Idle"));
                }

                hitParticle.Play();
                character.totallCollection++;
                CoinsManager.Instance.AddCoins(CoinsManager.Instance.WordPointToCanvasPoint(CoinsManager.Instance.mainCam, transform.position, CoinsManager.Instance.canvasRect), 1);
                CoinsManager.Instance.AddCoins(1);
                Debug.Log("Bullet");
            }
        }

        public void UpdateBar(float value)
        {
            if (healthBar.fillAmount <= 0.1)
            {
                if (canUpdate)
                {
                    Debug.Log("Bullet_000");

                    transform.DOLocalJump(jump.localPosition, 4, 1, 1.5f).OnComplete(() =>
                    {
                        gameObject.SetActive(false);
                    });

                    LizardSceneScenario();

                    if (Fight.Instance.activeSceneName == "InsectScene")
                    {
                        StartCoroutine(Delay());
                    }

                    this.GetComponent<BoxCollider>().enabled = false;
                }
            }

            // Only update if allowed
            if (canUpdate)
            {
                var temp = healthBar.fillAmount;
                temp += value;

                // Prevent going below 0
                if (temp < 0)
                {
                    temp = 0;
                }

                healthBar.DOFillAmount(temp, 1f).OnUpdate(() =>
                {
                    int healthPercentage = Mathf.CeilToInt(healthBar.fillAmount * 100);
                });

                int finalHealthPercentage = Mathf.CeilToInt(temp * 100);
            }
        }

        public void UpdateBarInLizradScene(float value)
        {
            if (lizardHealthBar.fillAmount < 0.7 && CurrentEnemyAttackState == 0)
            {
                character.gameObject.transform.GetComponent<THEBADDEST.CharacterController3.CharacterController>().enabled = false;

                DOVirtual.DelayedCall(1f, () => Fight.Instance.venom.SetTrigger("Attack"));
                DOVirtual.DelayedCall(0.5f, () => Fight.Instance.venom.SetTrigger("Idle"));

                DOVirtual.DelayedCall(2f, () =>
                {
                    CurrentEnemyAttackState = 1;
                    character.gameObject.SetActive(false);
                    DOVirtual.DelayedCall(1f, () => LizardFailSceneScenario());
                });

            }

            if (lizardHealthBar.fillAmount < 0.4 && CurrentEnemyAttackState == 1)
            {
                character.gameObject.transform.GetComponent<THEBADDEST.CharacterController3.CharacterController>().enabled = false;

                DOVirtual.DelayedCall(1f, () => Fight.Instance.venom.SetTrigger("Attack"));
                DOVirtual.DelayedCall(0.5f, () => Fight.Instance.venom.SetTrigger("Idle"));

                DOVirtual.DelayedCall(2f, () =>
                {
                    CurrentEnemyAttackState = 2;
                    character.gameObject.SetActive(false);
                    DOVirtual.DelayedCall(1f, () => LizardFailSceneScenario());
                });
            }

            if (lizardHealthBar.fillAmount <= 0 && CurrentEnemyAttackState == 2)
            {
                if (canUpdate)
                {
                    CurrentEnemyAttackState = 0;

                    transform.DOLocalJump(jump.localPosition, 4, 1, 1.5f).OnComplete(() =>
                    {
                        gameObject.SetActive(false);
                    });

                    LizardSceneScenario();

                    this.GetComponent<BoxCollider>().enabled = false;
                }
            }

            // Only update if allowed
            if (canUpdate)
            {
                var temp = lizardHealthBar.fillAmount;
                var temp1 = character.showLizardHealthBarOnPanel.fillAmount;
                temp += value;
                temp1 += value;

                // Prevent going below 0
                if (temp < 0)
                {
                    temp = 0;
                }

                // Prevent going below 0
                if (temp1 < 0)
                {
                    temp1 = 0;
                }

                lizardHealthBar.DOFillAmount(temp, 1f).OnUpdate(() =>
                {
                    int healthPercentage = Mathf.CeilToInt(lizardHealthBar.fillAmount * 100);
                    character.enemyHealthBar_Text.text = $"{healthPercentage}%";
                });

                character.showLizardHealthBarOnPanel.DOFillAmount(temp1, 1f).OnUpdate(() =>
                {
                    int healthPercentage = Mathf.CeilToInt(character.showLizardHealthBarOnPanel.fillAmount * 100);
                    character.enemyHealthBar_Text.text = $"{healthPercentage}%";
                });

                int finalHealthPercentage = Mathf.CeilToInt(temp * 100);
                character.enemyHealthBar_Text.text = $"{finalHealthPercentage}%";

                // Save the current health bar value to PlayerPrefs
                PlayerPrefs.SetFloat("CurrentHealtBarState", temp);
                PlayerPrefs.SetFloat("CurrentHealtBarState", temp1);
                Debug.Log($"Saved health bar state: {temp}");
                Debug.Log($"Saved health bar state: {temp1}");
            }
        }

        void LizardSceneScenario()
        {
            if (healthBar) healthBar.transform.parent.gameObject.SetActive(false);

            particleSystem.Play();
            canUpdate = false;

            if (Fight.Instance.activeSceneName == "LizardScene")
            {
                GameController.changeGameState(GameState.Complete);
                var lvl = PlayerPrefs.GetInt("LevelOfChara" + PlayerPrefManager.GetSceneName());
                if (lvl < character.insects.Count)
                {
                    lvl++;
                    PlayerPrefs.SetInt("LevelOfChara" + PlayerPrefManager.GetSceneName(), lvl);
                    Debug.Log($"Level updated to: {lvl}");
                }
            }
        }

        void LizardFailSceneScenario()
        {
            if (healthBar) healthBar.transform.parent.gameObject.SetActive(false);

            particleSystem.Play();
            canUpdate = false;

            if (Fight.Instance.activeSceneName == "LizardScene")
            {
                GameController.changeGameState(GameState.OnLizardLevelFail);
                var lvl = PlayerPrefs.GetInt("LevelOfChara" + PlayerPrefManager.GetSceneName());
                if (lvl < character.insects.Count)
                {
                    lvl++;
                    PlayerPrefs.SetInt("LevelOfChara" + PlayerPrefManager.GetSceneName(), lvl);
                    Debug.Log($"Level updated to: {lvl}");
                }
            }
        }

        IEnumerator Delay()
        {
            UpgardeManager.UpgradeFactorOfCharacter = 5;

            if (boolOfEnd == 0)
            {
                OneTime = 1;
                boolOfEnd = 1;
            }
            yield return new WaitForSeconds(1);
            GameController.changeGameState(GameState.Complete);
            yield return new WaitForSeconds(2);
        }

        int boolOfEnd
        {
            set { PlayerPrefs.SetInt("boolOfEnd", value); }
            get { return PlayerPrefs.GetInt("boolOfEnd"); }
        }

        public static int CurrentEnemyAttackState
        {
            set { PlayerPrefs.SetInt("EnemyAttackState", value); }
            get { return PlayerPrefs.GetInt("EnemyAttackState"); }
        }

        public static int OneTime
        {
            set { PlayerPrefs.SetInt("OneTime", value); }
            get { return PlayerPrefs.GetInt("OneTime"); }
        }
    }
}
