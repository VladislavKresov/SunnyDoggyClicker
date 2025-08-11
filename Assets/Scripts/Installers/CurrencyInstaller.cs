using ModestTree;
using SunnyDoggyClicker.Currencies;
using UnityEngine;
using Zenject;

public class CurrencyInstaller : MonoInstaller {
    [SerializeField] private CurrencyAsset[] _currencyAssets;
    private CurrencyBank _bank;

    public override void InstallBindings() {
        var bank = new CurrencyBank();
        if (_currencyAssets.Length > 0) {
            foreach (var currencyAsset in _currencyAssets) {
                var currency = currencyAsset.Currency;
                if (currency == null) {
                    Debug.LogError($"CurrencyBankInstaller::Empty currency -> Skipping [{_currencyAssets.IndexOf(currencyAsset)}] asset");
                    continue;
                }
                if (string.IsNullOrEmpty(currency.Name)) {
                    Debug.LogError($"CurrencyBankInstaller::Empty currency name -> Skipping [{_currencyAssets.IndexOf(currencyAsset)}] currency");
                    continue;
                }
                var currencyRegistered = false;
                if (currency.AmountLimitation > 0) {
                    currencyRegistered = bank.RegisterCurrency(currency.Name, currency.AmountLimitation);
                } else {
                    currencyRegistered = bank.RegisterCurrency(currency.Name);
                }
                if (currencyRegistered) {
                    if (currency.StartAmount > 0 && !bank.ExecuteTransaction(CurrencyTransaction.CreateAdd(currency.Name, currency.StartAmount))) {
                        Debug.LogError($"Validation failed: Can't add {currency.StartAmount} of {currency.Name}.");
                    }
                }
                else {
                    Debug.LogError($"Failed to regiser currency: Currency with name '{currency.Name}' is already registered.");
                }
            }
        }

        Container.Bind<CurrencyAsset[]>().FromInstance(_currencyAssets).AsSingle();
        Container.Bind<CurrencyBank>().FromInstance(bank).AsSingle().NonLazy();
    }
}
