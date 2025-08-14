using SunnyDoggyClicker.Audio;
using SunnyDoggyClicker.Signals;
using Zenject;

namespace SunnyDoggyClicker.UI.ViewModel {
    public class ClickerViewModel {
        private readonly ClickerModel _model;
        private readonly SignalBus _signalBus;
        private readonly SoundPlayer _soundPlayer;

        public ClickerViewModel(ClickerModel model, SignalBus signalBus, SoundPlayer soundPlayer) {
            _model = model;
            _signalBus = signalBus;
            _soundPlayer = soundPlayer;

            _signalBus.Subscribe<ClickerButtonUserClick>(OnUserClick);
            _signalBus.Subscribe<ClickerClick>(OnClickerClick);
        }

        private void OnClickerClick() {
            _soundPlayer.Play(1);
        }

        private void OnUserClick() {
            if (_model.TryClick()) {
                _soundPlayer.Play(1);
            }
        }
    }
}