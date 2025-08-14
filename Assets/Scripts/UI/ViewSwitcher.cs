using SunnyDoggyClicker.Audio;
using SunnyDoggyClicker.Signals;
using UnityEngine;
using Zenject;

namespace SunnyDoggyClicker.UI.View {
    public class ViewSwitcher : MonoBehaviour {
        [Inject] private readonly SoundPlayer _soundPlayer;
        [SerializeField] private View[] _ViewsToSwitch;

        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus) {
            _signalBus = signalBus;
            Initialize();
        }

        public void Initialize() {
            _signalBus.Subscribe<ShowViewSignal>(OnShowViewSignal);
            _ViewsToSwitch[0].Show();
        }

        private void OnShowViewSignal(ShowViewSignal view) {
            foreach (var screenView in _ViewsToSwitch) {
                if (screenView.Name == view.ViewName) {
                    screenView.Show();
                    _soundPlayer.Play(0);
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
