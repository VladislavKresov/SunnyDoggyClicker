using SunnyDoggyClicker.UI.Elements;
using SunnyDoggyClicker.Currencies;
using SunnyDoggyClicker.Signals;
using SunnyDoggyClicker.UI.ViewModel;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

namespace SunnyDoggyClicker.UI.View.ClickerView {
    public class ClickerView : View<ClickerViewModel> {
        public override string Name => nameof(ClickerView);

        [SerializeField] private CurrencyGuiInstance _currencyPrefab;
        [SerializeField] private RectTransform _currencyParent;
        [SerializeField] private Button _clickButton;

        [Inject] private readonly CurrencyAsset[] _currencyAssets;
        [Inject] private readonly CurrencyBank _currencyBank;
        [Inject] private readonly SignalBus _signalBus;

        protected override void OnAwake() {
            InitializeCurrencyUI();
            SetupClickButton();
        }

        private void InitializeCurrencyUI() {
            if (_currencyAssets != null) {
                foreach (var currencyAsset in _currencyAssets) {
                    var currencyInstance = Instantiate(_currencyPrefab, _currencyParent);
                    currencyInstance.gameObject.name = currencyAsset.Currency.Name;
                    currencyInstance.SetForCurrencyAsset(currencyAsset, _currencyBank);
                }
            }
        }

        private void SetupClickButton() {
            _clickButton?.onClick.AddListener(() =>
                _signalBus?.Fire(new ClickerButtonUserClick()));
        }

        protected override void OnShow() { }
        protected override void OnHide() { }
    }
}