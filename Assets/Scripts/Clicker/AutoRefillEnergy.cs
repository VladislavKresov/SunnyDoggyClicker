using SunnyDoggyClicker.Audio;
using SunnyDoggyClicker.Currencies;
using System.Collections;
using UnityEngine;
using Zenject;

namespace SunnyDoggyClicker {
    public class AutoRefillEnergy : MonoBehaviour {
        [Inject] private readonly ClickerModel _clicker;
        [Inject] private readonly ClickerSettings _clickerSettings;
        [Inject] private readonly CurrencyBank _bank;
        [Inject] private readonly SoundPlayer _soundPlayer;
        private Coroutine _autoRefillCoroutine;

        private void OnEnable() {
            CancelCoroutine();
            if (_clicker != null && _bank != null) {
                _autoRefillCoroutine = StartCoroutine(AutoRefill());
            }
        }

        private void CancelCoroutine() {
            if (_autoRefillCoroutine != null) {
                StopCoroutine(_autoRefillCoroutine);
                _autoRefillCoroutine = null;
            }
        }

        private void OnDisable() {
            CancelCoroutine();
        }

        private IEnumerator AutoRefill() {
            while (true) {
                yield return new WaitForSeconds(_clickerSettings.EnergyRefillDelay);
                if (_bank.ExecuteTransaction(CurrencyTransaction.CreateAdd("Energy", _clickerSettings.EnergyRefillAmount))) {
                    _soundPlayer.Play(2);
                }
            }
        }
    }
}