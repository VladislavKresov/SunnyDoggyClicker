using SunnyDoggyClicker.Currencies;
using SunnyDoggyClicker.Signals;
using UnityEngine;
using Zenject;

namespace SunnyDoggyClicker {
    public class Clicker {
        public readonly float CoinsPerClick;
        public readonly float ClickCostsEnergy;
        public readonly float AutoClickDelay;
        public readonly float EnergyRefillDelay;
        public readonly float EnergyRefillAmount;
        private readonly CurrencyBank _currencyBank;
        private readonly SignalBus _signalBus;

        public Clicker(ClickerSettings settings, SignalBus signalBus, CurrencyBank bank) {
            CoinsPerClick = settings.CoinsPerClick;
            ClickCostsEnergy = settings.ClickCostsEnergy;
            AutoClickDelay = settings.AutoClickDelay;
            EnergyRefillDelay = settings.EnergyRefillDelay;
            EnergyRefillAmount = settings.EnergyRefillAmount;
            _currencyBank = bank;
            _signalBus = signalBus;
            _signalBus.Subscribe<ClickerButtonUserClick>((c) => TryToClick());
        }

        public void TryToClick() {
            if (_currencyBank.ExecuteTransaction(CurrencyTransaction.CreateExchange("Energy", "Coin", ClickCostsEnergy, CoinsPerClick / ClickCostsEnergy))) {
                _signalBus.Fire(new ClickerClick());
            }
        }
    }

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