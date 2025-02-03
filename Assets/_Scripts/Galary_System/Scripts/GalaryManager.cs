using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ThemeSystem.FIghtingSystem;
using Sirenix.OdinInspector;
using ThemeSystem;
using DG.Tweening;
using System;
using GameAssets.GameSet.GameDevUtils.Managers;

namespace GalarySystem
{
    public class GalaryManager : MonoBehaviour
    {
        public static event Action<int> onCharacterUpdate;
        public static event Action<int> onSpecialItemUpdate;

        [FoldoutGroup("Insect Data")]
        [SerializeField] GalleryInsectData galleryInsectData;
        [FoldoutGroup("Insect Data")]
        [SerializeField] GalleryInsectData specialInsectData;

        [FoldoutGroup("Galary_Items")]
        [SerializeField] GalaryItem[] characterItems;
        [Space]
        [FoldoutGroup("Galary_Items")]
        [SerializeField] GalaryItem[] specialItems;

        [FoldoutGroup("Gallery Content")]
        [SerializeField] RectTransform characterContent;
        [Space]
        [FoldoutGroup("Gallery Content")]
        [SerializeField] RectTransform specialItemContent;
        [Space]
        [FoldoutGroup("Gallery Content")]
        [SerializeField] Sprite contentOffButtonSprite;
        [Space]
        [FoldoutGroup("Gallery Content")]
        [SerializeField] Sprite contentOnButtonSprite;

        [BoxGroup("----- Galary Refrence -----", centerLabel: true)]
        [SerializeField] Button galaryButton;
        [Space]
        [BoxGroup("----- Galary Refrence -----")]
        [SerializeField] Button charcaterSelectionButton;
        [Space]
        [BoxGroup("----- Galary Refrence -----")]
        [SerializeField] Button specialItemSelectionButton;
        [Space]
        [BoxGroup("----- Galary Refrence -----")]
        [SerializeField] GameObject main_galary;
        [Space]
        [BoxGroup("----- Galary Refrence -----")]
        [SerializeField] ScrollRect scrollRect;
        [Space]
        [BoxGroup("----- Galary Refrence -----")]
        [SerializeField] GameObject gallaryIndecation;
        [Space]
        [BoxGroup("----- Select Character -----")]
        [SerializeField] public Image selectedCharacter;

        [BoxGroup("----- Select Item -----")]
        [SerializeField] GalaryItem currentGalaryItem;
        [BoxGroup("----- Select Item -----")]
        [SerializeField] GalaryItem currentSpecialItem;
        [BoxGroup("----- Purches Cash Data -----")]
        [SerializeField] Text purchesCashText;

        [FoldoutGroup("PopUp Data")]
        [SerializeField] Image specialPopUpItem;
        [FoldoutGroup("PopUp Data")]
        [SerializeField] GameObject noThank;
        [FoldoutGroup("PopUp Data")]
        [SerializeField] GameObject Equip;
        [FoldoutGroup("PopUp Data")]
        [SerializeField] GameObject specialPopUpPanel;
        [FoldoutGroup("PopUp Data")]
        [SerializeField] GameObject adButton;

        bool unlockPanel = false;
        bool galaryPanel = false;

        private void Awake()
        {
            selectedCharacter.gameObject.SetActive(true);
            UpgardeManager.onCharacterUpGrade += SelectedCharacter;

            if (GameStart == 0)
            {
                GameStart = 1;
                PurchesCash = 20;
                TotalSpecialItemUnlock = 1;
            }

            Initialize();
            purchesCashText.text = PurchesCash.ToString();
        }

        private void Start()
        {
            if (CoinsManager.Instance.Coins >= 20 && GallaryIndecator == 0)
            {
                gallaryIndecation.SetActive(true);
                GallaryIndecator = 1;
            }
            selectedCharacter.gameObject.SetActive(true);
        }

        public void Initialize()
        {
            //var id = TotalSpecialItemUnlock;
            SetCurrentItem(TotalSpecialItemUnlock);
            Validate();
            AddListeners();
            GameController.onHome += OnGameStart;
            CharacterContent();

            UpGradeSpecialItem(SpecialItemId);

            if (PlayerPrefs.GetInt("LevelOfChara" + PlayerPrefManager.GetSceneName()) - 1 < 0)
            {
                SelectedCharacter(0);
            }
            else
            {
                SelectedCharacter(PlayerPrefs.GetInt("LevelOfChara" + PlayerPrefManager.GetSceneName()) - 1);
            }

            UnlockSpecialItem(TotalSpecialItemUnlock);
        }

        private void OnGameStart()
        {
            LoadGalaryItems();
        }

        private void LoadGalaryItems()
        {
            for (int i = 0; i < characterItems.Length; i++)
            {
                if (characterItems[i].IsDataSaved) { continue; }
                characterItems[i].SetData(galleryInsectData.insectPicture[i]);
            }
            for (int i = 0; i < specialItems.Length; i++)
            {
                if (specialItems[i].IsDataSaved) { continue; }
                specialItems[i].SetData(specialInsectData.insectPicture[i]);
            }
        }

        private void AddListeners()
        {
            if (galaryButton) { galaryButton.onClick.AddListener(OpenGalary); } else { Debug.LogError("Galary Button Not Assigned"); }
            if (charcaterSelectionButton) { charcaterSelectionButton.onClick.AddListener(CharacterContent); } else { Debug.LogError("Galary Button Not Assigned"); }
            if (specialItemSelectionButton) { specialItemSelectionButton.onClick.AddListener(SpecialItemContent); } else { Debug.LogError("Galary Button Not Assigned"); }
        }

        private void OpenGalary()
        {
            ActivePanel(0);
            SnapTo(PlayerPrefs.GetInt("LevelOfChara" + PlayerPrefManager.GetSceneName()) - 1, characterItems.Length);
            if (currentGalaryItem)
                characterItems[currentGalaryItem.idCharacter + 1].RestPrice(UpgardeManager.UpgardePrice);
        }

        #region Popup Panels Code

        public void ActivePanel(int type)
        {
            switch (type)
            {
                case 0:
                    ChangePanelState(ref galaryPanel, main_galary);
                    break;
                //case 1:
                //    ChangeContent();
                //    break;
                default:
                    Debug.Log("Wrong Panel Index ");
                    break;
            }
        }
        void CharacterContent()
        {
            selectedCharacter.gameObject.SetActive(true);
            //SnapTo(PlayerPrefs.GetInt("LevelOfChara" + PlayerPrefManager.GetSceneName()) - 1, characterItems.Length);
            characterContent.gameObject.SetActive(true);
            specialItemContent.gameObject.SetActive(false);
            specialItemSelectionButton.GetComponent<Image>().sprite = contentOffButtonSprite;
            charcaterSelectionButton.GetComponent<Image>().sprite = contentOnButtonSprite;
            scrollRect.content = characterContent;
            if (PlayerPrefs.GetInt("LevelOfChara" + PlayerPrefManager.GetSceneName()) - 1 < 0)
            {
                selectedCharacter.sprite = galleryInsectData.insectPicture[0].insectPicture;
                SnapTo(0, characterItems.Length);
            }
            else
            {
                SnapTo(PlayerPrefs.GetInt("LevelOfChara" + PlayerPrefManager.GetSceneName()) - 1, characterItems.Length);
                selectedCharacter.sprite = galleryInsectData.insectPicture[PlayerPrefs.GetInt("LevelOfChara" + PlayerPrefManager.GetSceneName()) - 1].insectPicture;

            }
        }
        void SpecialItemContent()
        {
            characterContent.gameObject.SetActive(false);
            specialItemContent.gameObject.SetActive(true);
            charcaterSelectionButton.GetComponent<Image>().sprite = contentOffButtonSprite;
            specialItemSelectionButton.GetComponent<Image>().sprite = contentOnButtonSprite;
            scrollRect.content = specialItemContent;
            SnapTo(SpecialItemId, specialItems.Length);
            if (SpecialItemUnlock == 1)
            {

                selectedCharacter.sprite = specialInsectData.insectPicture[SpecialItemId].insectPicture;
            }
            else
            {
                selectedCharacter.gameObject.SetActive(false);
            }
        }
        public void ChangePanelState(ref bool isActive, GameObject panel)
        {
            isActive = (isActive == false) ? true : false;
            if (isActive)
            {
                panel.SetActive(isActive);
                panel.transform.GetChild(1).GetComponent<DOTweenAnimation>().DORestartById("out");
            }
            else
            {
                // panel will disable in On complete deleget in editor 
                panel.transform.GetChild(1).GetComponent<DOTweenAnimation>().DORestartById("in");
            }
            //SoundManager.instance.PlaySFX(SoundManager.instance.uiOpen, 0.2f);
        }
        #endregion

        private void Validate()
        {
            if (galleryInsectData.insectPicture.Length != characterItems.Length)
            {
                Debug.LogError("Fighter Data And Galary Items List Count Does Not Match !!!!! SAFE Retruning ");
                return;
            }
        }

        public static int MaxMergedCharacter
        {
            get { return PlayerPrefs.GetInt("LevelOfChara" + PlayerPrefManager.GetSceneName()); }

        }

        int GameStart
        {
            get { return PlayerPrefs.GetInt("GameStart"); }
            set { PlayerPrefs.SetInt("GameStart", value); }
        }

        void SelectedCharacter(int index)
        {
            if (currentGalaryItem)
            {
                currentGalaryItem.ResetSelectedOuter();
            }

            characterItems[index].SetUnlockState(galleryInsectData.insectPicture[index]);
            selectedCharacter.sprite = galleryInsectData.insectPicture[index].insectPicture;
            currentGalaryItem = characterItems[index];
            UnlockNext4Character(index);
        }

        void SelectedSpecialItem(int index)
        {
            //Debug.Log("SpecialCharacter");
            selectedCharacter.gameObject.SetActive(true);
            if (currentSpecialItem)
            {
                currentSpecialItem.ResetSelectedOuter();
            }

            specialItems[index].SetUnlockState(specialInsectData.insectPicture[index]);

            if (index == 0)
            {
                selectedCharacter.gameObject.SetActive(false);
            }
            else
            {
                selectedCharacter.sprite = specialInsectData.insectPicture[index].insectPicture;
            }
            currentSpecialItem = specialItems[index];
            // UnlockNext4Character(index);
        }

        public void SetOuter(int index)
        {
            characterItems[index].ResetSelectedOuter();
        }

        public void SnapTo(int index, int length)
        {
            gallaryIndecation.SetActive(false);
            float value = ((float)index / (float)length);

            //Debug.Log("Value:" + index);
            DOVirtual.Float(scrollRect.verticalScrollbar.value, 1 - value, 2f, tempValue =>
              {
                  scrollRect.verticalScrollbar.value = tempValue;
              });
        }

        public void UpGradeChaacter(int id)
        {
            onCharacterUpdate?.Invoke(id);
        }

        public void UpGradeSpecialItem(int id)
        {
            SpecialItemId = id;
            onSpecialItemUpdate?.Invoke(id);
            SelectedSpecialItem(id);
        }

        public bool ContainId(int id)
        {
            if (currentGalaryItem.idCharacter == id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ContainSpecialCharacterId(int id)
        {
            if (currentSpecialItem.idCharacter == id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ShowRewardedVideo()
        {
            //_Sajid_Saingal AdController.instance?.ShowRewarded(AddCash);
        }

        void AddCash()
        {

            //_Sajid_Saingal FirebaseConnection.instance?.Check_RewardedAdsOn_StoreCash();

            CoinsManager.Instance.AddCoins(PurchesCash);
            CoinsManager.Instance.AddCoinOnGamePlay();
            UpgardeManager.instace.InitialCash();
            PurchesCash += 5;
            purchesCashText.text = PurchesCash.ToString();
            CoinsManager.Instance.AddCoins(CoinsManager.Instance.WordPointToCanvasPoint(CoinsManager.Instance.mainCam, ReferenceManager.instance.characterController.transform.position, CoinsManager.Instance.canvasRect), 10);
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.cash, 1);
        }

        void UnlockNext4Character(int id)
        {
            int totalCHaracter = id + 4;
            for (int i = 0; i < totalCHaracter; i++)
            {
                characterItems[i].lockSprite.SetActive(false);
                characterItems[i].selectionButton.interactable = true;
            }
            if (currentGalaryItem)
                characterItems[currentGalaryItem.idCharacter + 1].RestPrice(UpgardeManager.UpgardePrice);
        }

        void UnlockSpecialItem(int index)
        {
            for (int i = 0; i < TotalSpecialItemUnlock; i++)
            {
                specialItems[i].lockSprite.SetActive(false);
                specialItems[i].selectionButton.interactable = true;
            }
        }

        void SetCurrentItem(int index)
        {
            specialPopUpItem.sprite = specialInsectData.insectPicture[index].insectPicture;
        }

        public static int SpecialItemId
        {
            get { return PlayerPrefs.GetInt("SpecialItemId"); }
            set { PlayerPrefs.SetInt("SpecialItemId", value); }
        }

        public static int PurchesCash
        {

            get { return PlayerPrefs.GetInt("PurchesCash"); }
            set { PlayerPrefs.SetInt("PurchesCash", value); }
        }

        public static int SpecialItemUnlock
        {
            get { return PlayerPrefs.GetInt("SpecialItemUnlock"); }
            set { PlayerPrefs.SetInt("SpecialItemUnlock", value); }
        }

        public static int TotalSpecialItemUnlock
        {
            get { return PlayerPrefs.GetInt("TotalSpecialItemUnlock"); }
            set { PlayerPrefs.SetInt("TotalSpecialItemUnlock", value); }
        }

        public void PlayAd()
        {
            UnlockItem();
            //_Sajid_Saingal  AdController.instance?.ShowRewarded(UnlockItem);
        }

        int GallaryIndecator
        {
            get { return PlayerPrefs.GetInt("GallaryIndecator"); }
            set { PlayerPrefs.SetInt("GallaryIndecator", value); }
        }

        private void OnDestroy()
        {
            onCharacterUpdate = null;
        }

        public void UnlockItem()
        {

            //_Sajid_Saingal   FirebaseConnection.instance?.Check_RewardedAdsOn_HeadGearsItem();

            specialItems[TotalSpecialItemUnlock].Lock = 1;

            Equip.SetActive(true);
            noThank.SetActive(false);
            adButton.SetActive(false);
            SpecialItemId = TotalSpecialItemUnlock;
            TotalSpecialItemUnlock++;

            ReferenceManager.instance.uIManager.CompletePanel();
        }
    }
}