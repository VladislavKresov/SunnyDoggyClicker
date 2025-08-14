using SunnyDoggyClicker.Signals;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Zenject;

namespace SunnyDoggyClicker.UI.ViewModel {
    public class DogsViewModel {
        private const string API_BREEDS = "https://dogapi.dog/api/v2/breeds";
        private readonly SignalBus _signalBus;
        private CancellationTokenSource _cts;
        private RequestQueue<string, DogBreed[]> _breedsQueue;
        private RequestQueue<string, DogBreed[]>.QueueItem _currentBreedsRequest;
        private RequestQueue<string, DogBreed> _breedQueue;
        private RequestQueue<string, DogBreed>.QueueItem _currentBreedRequest;

        public Action<DogBreed[]> BreedsReceived;

        public DogsViewModel(SignalBus signalBus) {
            _breedsQueue = new RequestQueue<string, DogBreed[]>(RequestBreedsProcessor);
            _breedQueue = new RequestQueue<string, DogBreed>(RequestBreedProcessor);
            _cts = new CancellationTokenSource();
            _signalBus = signalBus;
            _signalBus.Subscribe<LoadBreed>(LoadSignal);
        }

        private void LoadSignal(LoadBreed signal) {
            RequestBreedById(signal.BreedId);
        }

        public async void RequestAllBreeds() {
            var breeds = await RequestBreeds();
            if (breeds != null) {
                BreedsReceived?.Invoke(breeds);
            }
        }

        public async void RequestBreedById(string id) {
            _signalBus.Fire(new BreedLoading() { BreedId = id });
            var breed = await RequestBreed(id);
            if (breed != null) {
                _signalBus.Fire(new BreedLoaded() { Breed = breed });
            }
        }

        private async Task<DogBreed> RequestBreed(string id) {
            if (_currentBreedRequest != null) {
                _breedsQueue.TryCancel(_currentBreedRequest.Id);
            }

            try {
                _currentBreedRequest = await _breedQueue.EnqueueAsync(id, _cts.Token);
                return await _currentBreedRequest.Task;
            }
            catch (Exception ex) {
                return null;
            }
        }

        private async Task<DogBreed[]> RequestBreeds() {
            if (_currentBreedsRequest != null) {
                _breedsQueue.TryCancel(_currentBreedsRequest.Id);
            }

            try {
                _currentBreedsRequest = await _breedsQueue.EnqueueAsync(API_BREEDS, _cts.Token);
                return await _currentBreedsRequest.Task;
            }
            catch (Exception ex) {
                return null;
            }
        }

        public void CancelCurrentRequest() {
            _cts.Cancel();
            _cts = new CancellationTokenSource();
        }

        private async Task<DogBreed[]> RequestBreedsProcessor(string url, CancellationToken cts) {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url, cts);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return DogBreed.GetBreedsFromJson(json);
        }

        private async Task<DogBreed> RequestBreedProcessor(string id, CancellationToken cts) {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{API_BREEDS}/{id}", cts);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return DogBreed.GetBreedFromJson(json);
        }
    }
}
