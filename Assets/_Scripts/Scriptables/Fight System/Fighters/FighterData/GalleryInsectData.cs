using GalarySystem;
using UnityEngine;

namespace ThemeSystem.FIghtingSystem
{
    [CreateAssetMenu(menuName = "Theme System/Insect Information")]
    public class GalleryInsectData : ScriptableObject
    {
        public InsectProfile[] insectPicture;
    }

    [System.Serializable]
    public class InsectProfile
    {
        public PaymentMethod paymentMethod;
        public Sprite insectPicture;
        public int price;
        public Sprite cashSprite;
        public Sprite adSprite;
    }
}