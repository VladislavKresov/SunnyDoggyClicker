using System.Collections;
using UnityEngine;
using Zenject;

namespace SunnyDoggyClicker {
    public class AutoClick : MonoBehaviour {
        [Inject] private readonly ClickerModel _clicker;
        [Inject] private readonly ClickerSettings _clickerSettings;
        private Coroutine _autoClickCoroutine;

        private void OnEnable() {
            CancelAutoClickCoroutine();
            if (_clicker != null) {
                _autoClickCoroutine = StartCoroutine(AutoClickButton());
            }
        }

        private void CancelAutoClickCoroutine() {
            if (_autoClickCoroutine != null) {
                StopCoroutine(_autoClickCoroutine);
                _autoClickCoroutine = null;
            }
        }

        private void OnDisable() {
            CancelAutoClickCoroutine();
        }

        private IEnumerator AutoClickButton() {
            while (true) {
                yield return new WaitForSeconds(_clickerSettings.AutoClickDelay);
                _clicker.TryClick();
            }
        }
    }
}