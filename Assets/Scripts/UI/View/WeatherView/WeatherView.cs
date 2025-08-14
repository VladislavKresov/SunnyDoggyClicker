using SunnyDoggyClicker.UI.ViewModel;
using UnityEngine;
using UnityEngine.UI;

namespace SunnyDoggyClicker.UI.View.Weather {
    public class WeatherView : View<WeatherViewModel> {
        [SerializeField] private Text _forecastLabel;
        private WeatherForecast _tempForecast;

        public override string Name => nameof(WeatherView);

        protected override void OnAwake() {
            ViewModel.WeatherForecastReceived += OnWeatherForecastReceived;
        }

        private void OnWeatherForecastReceived(WeatherForecast forecast) {
            _tempForecast = forecast;
        }

        protected override void OnShow() {
            ViewModel.RequestWeather();
            ViewModel.StartRequestWeatherWithDelay();
        }

        protected override void OnHide() {
            ViewModel.CancelCurrentRequest();
        }

        private void Update() {
            if (_tempForecast != null) {
                _forecastLabel.text = _tempForecast.Temperature + _tempForecast.TemperatureUnit;
                _tempForecast = null;
            }
        }
    }
}