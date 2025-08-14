using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace SunnyDoggyClicker.UI.ViewModel {
    public class WeatherViewModel {
        public const string API = "https://api.weather.gov/gridpoints/TOP/31,80/forecast";
        private readonly RequestQueue<string, WeatherForecast> _queue;
        private CancellationTokenSource _cts;
        private RequestQueue<string, WeatherForecast>.QueueItem _currentRequest;
        public Action<WeatherForecast> WeatherForecastReceived;
        private static System.Timers.Timer _timer;

        public WeatherViewModel() {
            _queue = new RequestQueue<string, WeatherForecast>(RequestProcessor);
            _cts = new CancellationTokenSource();
        }

        private async Task<WeatherForecast> RequestProcessor(string url, CancellationToken cancellationToken) {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "myweatherapp.com (contact@myweatherapp.com)");
            var response = await httpClient.GetAsync(url, cancellationToken);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return WeatherForecast.FromJson(json);
        }

        public async void RequestWeather() {
            var forecast = await RequestWeatherForecast();
            if (forecast != null) {
                WeatherForecastReceived?.Invoke(forecast);
            }
        }

        public void StartRequestWeatherWithDelay(int milliseconds = 5000) {
            _timer = new System.Timers.Timer(milliseconds);
            _timer.Elapsed += OnTimer;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private async void OnTimer(object sender, ElapsedEventArgs e) {
            RequestWeather();
        }

        public void CancelCurrentRequest() {
            _timer.Stop();
            _timer.Dispose();
            _cts.Cancel();
            _cts = new CancellationTokenSource();
        }

        private async Task<WeatherForecast> RequestWeatherForecast() {
            if (_currentRequest != null) {
                _queue.TryCancel(_currentRequest.Id);
            }

            try {
                _currentRequest = await _queue.EnqueueAsync(API, _cts.Token);
                return await _currentRequest.Task;
            }
            catch (Exception ex) {
                return null;
            }
        }
    }
}