using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ThemeSystem.FIghtingSystem;
using Sirenix.OdinInspector;
using DG.Tweening;
using GameAssets.GameSet.GameDevUtils.Managers;

namespace GalarySystem
{
    public class GalaryItem : MonoBehaviour
    {
        [Header("        ----- UI Refrences -----")]

        [SerializeField] GameObject container;
        [SerializeField] Image paymentSprite;

        [Space]
        [SerializeField] Image characterImage;
        [Space]
        [Space]
        [SerializeField] DOTweenAnimation animation;
        [Space]

        [Space]
        [SerializeField] public GameObject lockSprite;

        [Space]
        [SerializeField] GameObject outerCharacterImage;
        [Space]
        [SerializeField] Text cashPriceText;

        [Space]
        [SerializeField] public int idCharacter;

        [Space]
        [SerializeField] int price;

        [Space]
        public PaymentMethod paymentMethod;

        [Space]
        [BoxGroup("----- Galary Refrence -----")]
        [SerializeField] public Button selectionButton;
        [Space]
        [BoxGroup("----- Galary Items -----")]
        [SerializeField] public StoreItemType storeItemType;
        private void Awake()
        {
            idCharacter = transform.GetSiblingIndex();
            if (storeItemType == StoreItemType.Character)
            {

                if (selectionButton) { selectionButton.onClick.AddListener(OnClick); } else { Debug.LogError("Galary Button Not Assigned"); }
            }
            else
            {

                if (selectionButton) { selectionButton.onClick.AddListener(OnItemClick); } else { Debug.LogError("Galary Button Not Assigned"); }
            }

        }
        bool dataSaved;

        public bool IsDataSaved { get => dataSaved; }

        public void SetData(InsectProfile insectProfile)
        {
            paymentMethod = insectProfile.paymentMethod;
            if (Lock == 0)
            {
                if (paymentMethod == PaymentMethod.Cash)
                {
                    if (paymentSprite) paymentSprite.sprite = insectProfile.cashSprite;
                    if (cashPriceText) cashPriceText.text = insectProfile.price.ToString();
                    price = insectProfile.price;
                }
                else
                {
                    if (paymentSprite) paymentSprite.sprite = insectProfile.adSprite;
                    if (cashPriceText) cashPriceText.gameObject.SetActive(false);
                }
                if (paymentSprite) paymentSprite.gameObject.SetActive(true);
                if (characterImage) characterImage.sprite = insectProfile.insectPicture;

            }
            else
            {

                if (characterImage) characterImage.sprite = insectProfile.insectPicture;
                if (paymentSprite) paymentSprite.gameObject.SetActive(false);
                if (cashPriceText) cashPriceText.gameObject.SetActive(false);
            }

            dataSaved = true;
        }

        void CheckUnlockMethod()
        {
            if (paymentMethod == PaymentMethod.Cash)
            {
                UnlockOnCash();
            }
            else
            {
                UnlockOnAd();
            }
        }

        void UnlockOnCash()
        {
            if (CoinsManager.Instance.CanDoTransaction(price))
            {
                ChangeLockState();
                CoinsManager.Instance.DecCoins(price);
            }
        }

        void UnlockOnAd()
        {
            //_Sajid_Saingal  AdController.instance?.ShowRewarded(ChangeLockState);
        }

        void ChangeLockState()
        {
            Lock = 1;
            if (paymentSprite) paymentSprite.gameObject.SetActive(false);
            if (storeItemType == StoreItemType.Character)
            {
                ReferenceManager.instance.galaryManager.UpGradeChaacter(idCharacter);
            }
            else
            {

                //_Sajid_Saingal  FirebaseConnection.instance?.Check_RewardedAdsOn_HeadGearsItemOnStore();

                GalaryManager.SpecialItemUnlock = 1;
                ReferenceManager.instance.galaryManager.UpGradeSpecialItem(idCharacter);
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.upGrade, 1);
            }

        }

        public void SetUnlockState(InsectProfile insectProfile)
        {
            Lock = 1;
            SetData(insectProfile);
            outerCharacterImage.SetActive(true);
        }

        public void ResetSelectedOuter()
        {
            outerCharacterImage.SetActive(false);
        }

        void OnClick()
        {
            Debug.Log("OnCick");
            animation.DORestart();
            if (ReferenceManager.instance.galaryManager.ContainId(idCharacter)) return;
            if (Lock == 1)
            {
                ReferenceManager.instance.galaryManager.UpGradeChaacter(idCharacter);
                outerCharacterImage.SetActive(true);
            }
            else
            {
                CheckUnlockMethod();
            }
        }

        void OnItemClick()
        {
            animation.DORestart();

            if (Lock == 1)
            {
                ReferenceManager.instance.galaryManager.UpGradeSpecialItem(idCharacter);
                outerCharacterImage.SetActive(true);
            }
            else
            {
                CheckUnlockMethod();
            }
        }

        public void RestPrice(int price)
        {
            this.price = price;
            cashPriceText.text = this.price.ToString();
        }

        public int Lock
        {
            get { return PlayerPrefs.GetInt(this.name); }
            set { PlayerPrefs.SetInt(this.name, value); }
        }
    }

    public enum PaymentMethod
    {
        Cash,
        Ads
    }

    public enum StoreItemType
    {
        Character,
        SpecialItem
    }
}