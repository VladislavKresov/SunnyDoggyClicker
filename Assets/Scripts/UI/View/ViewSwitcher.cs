using SunnyDoggyClicker.Signals;
using UnityEngine;
using Zenject;

namespace SunnyDoggyClicker.UI.View {
    public class ViewSwitcher : MonoBehaviour {
        [SerializeField] private View[] _ViewsToSwitch;

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
            foreach (var screenView in _ViewsToSwitch) {
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
