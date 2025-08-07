using UnityEngine;
using Zenject;

namespace SunnyDoggyClicker.UI.Navigation {
    public class ViewScreensSwitcher : MonoBehaviour {
        [SerializeField] private View[] _screensToSwitch;

        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus) {
            _signalBus = signalBus;
            Initialize();
        }

        public void Initialize() {
            _signalBus.Subscribe<ShowViewSignal>(OnShowViewSignal);
        }

        private void OnShowViewSignal(ShowViewSignal view) {
            foreach (var screenView in _screensToSwitch) {
                if (screenView.Name == view.ViewName) {
                    if (!screenView.IsShown) {
                        screenView.Show();
                    }
                }
                else {
                    if (screenView.IsShown) {
                        screenView.Hide();
                    }
                }
            }
        }

        public void Dispose() {
            _signalBus.Unsubscribe<ShowViewSignal>(OnShowViewSignal);
        }
    }
}
