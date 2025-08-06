using UnityEngine;
using Zenject;

namespace SunnyDoggyClicker.UI.Navigation {
    public class ViewScreensSwitcher : MonoBehaviour {
        [SerializeField] private View[] Screens;

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
            foreach (var screenView in Screens) {
                if (screenView.Name == view.ViewName) {
                    screenView.Show();
                }
                else {
                    screenView.Hide();
                }
            }
        }

        public void Dispose() {
            _signalBus.Unsubscribe<ShowViewSignal>(OnShowViewSignal);
        }
    }
}
