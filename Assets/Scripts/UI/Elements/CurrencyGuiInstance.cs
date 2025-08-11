using SunnyDoggyClicker.Currencies;

namespace SunnyDoggyClicker.UI.Elements {
    public class CurrencyGuiInstance : ImageWithText {
        private CurrencyBank _currencyBank;
        private string _currencyName;
        private bool _isInitialized = false;

        public void SetForCurrencyAsset(CurrencyAsset asset, CurrencyBank currencyBank) {
            _currencyBank = currencyBank;
            SetSprite(asset.Sprite);
            SetTextColor(asset.TextColor);
            _currencyName = asset.Currency.Name;
            _isInitialized = true;
        }

        private void Update() {
            if (_isInitialized) {
                SetText($"{_currencyBank.GetBalance(_currencyName)}"); //TODO: need to update only when balance is updated
            }
        }
    }
}