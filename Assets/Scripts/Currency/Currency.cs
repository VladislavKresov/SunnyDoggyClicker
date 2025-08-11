using UnityEngine;

namespace SunnyDoggyClicker.Currencies {
    [CreateAssetMenu(fileName = "CurrencyDefinition", menuName = "SunnyDoggyClicker/Currency Definition")]
    public class Currency : ScriptableObject {
        public string Name;
        public float AmountLimitation = 0f;
        public float StartAmount = 0f;
    }

    [CreateAssetMenu(fileName = "CurrencyAsset", menuName = "SunnyDoggyClicker/UI/Currency Asset")]
    public class CurrencyAsset : ScriptableObject {
        public Currency Currency;
        public Sprite Sprite;
        public Color TextColor;
    }
}