using SunnyDoggyClicker.Signals;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SunnyDoggyClicker.UI.Elements {
    public class BreedItem : MonoBehaviour {
        [SerializeField] private Text _indexText;
        [SerializeField] private Text _nameText;
        [SerializeField] private Button _button;
        [SerializeField] private Image _loadingImage;

        private string _dogBreedId;
        private SignalBus _signalBus;

        public void Set(int index, DogBreed breed, SignalBus signalBus) {
            _indexText.text = index.ToString();
            _nameText.text = breed.Name;
            _dogBreedId = breed.Id;
            _signalBus = signalBus;
        }

        private void OnBreedLoading(BreedLoading signal) {
            _loadingImage.gameObject.SetActive(_dogBreedId == signal.BreedId);
        }

        private void OnBreedLoaded(BreedLoaded signal) {
            _loadingImage.gameObject.SetActive(false);
        }

        private void Start() {
            _button.onClick.AddListener(() => _signalBus.Fire(new LoadBreed() { BreedId = _dogBreedId }));
            _loadingImage.gameObject.SetActive(false);
            _signalBus.Subscribe<BreedLoading>(OnBreedLoading);
            _signalBus.Subscribe<BreedLoaded>(OnBreedLoaded);
        }

        private void OnDestroy() {
            _signalBus.Unsubscribe<BreedLoading>(OnBreedLoading);
            _signalBus.Unsubscribe<BreedLoaded>(OnBreedLoaded);
        }
    }
}