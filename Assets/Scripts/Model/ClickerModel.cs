using SunnyDoggyClicker.Currencies;
using SunnyDoggyClicker.Signals;
using Zenject;

namespace SunnyDoggyClicker {
    public class ClickerModel {
        public readonly float CoinsPerClick;
        public readonly float ClickCostsEnergy;

        private readonly CurrencyBank _currencyBank;
        private readonly SignalBus _signalBus;

        public ClickerModel(ClickerSettings settings, SignalBus signalBus, CurrencyBank bank) {
            CoinsPerClick = settings.CoinsPerClick;
            ClickCostsEnergy = settings.ClickCostsEnergy;
            _currencyBank = bank;
            _signalBus = signalBus;
        }

        public bool TryClick() {
            if (_currencyBank.ExecuteTransaction(
                CurrencyTransaction.CreateExchange("Energy", "Coin", ClickCostsEnergy, CoinsPerClick / ClickCostsEnergy))) {
                _signalBus.Fire(new ClickerClick());
                return true;
            }
            return false;
        }
    }
}