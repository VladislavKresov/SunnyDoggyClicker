using UnityEngine;

namespace SunnyDoggyClicker {
    [CreateAssetMenu(fileName = "ClickerSettings", menuName = "SunnyDoggyClicker/Clicker Settings")]
    public class ClickerSettings : ScriptableObject {
        [Header("Cliker")]
        public float CoinsPerClick = 1f;
        public float ClickCostsEnergy = 1f;
        public float AutoClickDelay = 3f;
        [Space, Header("Energy")]
        public float EnergyRefillDelay = 10f;
        public float EnergyRefillAmount = 10f;
    }
}