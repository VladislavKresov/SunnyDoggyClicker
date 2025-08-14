using SunnyDoggyClicker.Currencies;

namespace SunnyDoggyClicker.UI.Elements {
    public class CurrencyGuiInstance : ImageWithText {
        private CurrencyBank _currencyBank;
        private string _currencyName;
        private bool _isInitialized = false;
        private DOScaleAnimation _scaleAnimation;

        public void SetForCurrencyAsset(CurrencyAsset asset, CurrencyBank currencyBank) {
            _currencyBank = currencyBank;
            SetSprite(asset.Sprite);
            SetTextColor(asset.TextColor);
            _currencyName = asset.Currency.Name;
            _isInitialized = true;
            currencyBank.BalanceChanged += OnBalanceChanged;
        }

        public override void SetText(string text) {
            base.SetText(text);
            if (_scaleAnimation == null) {
                _scaleAnimation = base.text?.GetComponent<DOScaleAnimation>() ?? base.text?.gameObject.AddComponent<DOScaleAnimation>();
            }
            _scaleAnimation.Play();
        }

        private void OnEnable() {
            if (_currencyBank != null) {
                _currencyBank.BalanceChanged += OnBalanceChanged;
            }
        }

        private void OnDisable() {
            if (_currencyBank != null) {
                _currencyBank.BalanceChanged -= OnBalanceChanged;
            }
        }

        private void OnBalanceChanged(string currency, float newValue) {
            if (currency == _currencyName) {
                SetText($"{newValue}");
            }
        }
    }
}