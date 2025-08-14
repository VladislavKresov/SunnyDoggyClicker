using SunnyDoggyClicker.UI.Elements;
using SunnyDoggyClicker.UI.ViewModel;
using UnityEngine;
using Zenject;

namespace SunnyDoggyClicker.UI.View.Dogs {
    public class DogsView : View<DogsViewModel> {
        [Inject] private readonly SignalBus _signalBus;
        [SerializeField] private BreedItem _breedPrefab;
        [SerializeField] private RectTransform _itemsParent;
        private DogBreed[] _breedsTemp;

        public override string Name => nameof(DogsView);

        protected override void OnAwake() {
            ViewModel.BreedsReceived += (breeds) => _breedsTemp = breeds;
        }

        protected override void OnShow() {
            ViewModel.RequestAllBreeds();
        }

        protected override void OnHide() {
            ViewModel.CancelCurrentRequest();
            DestroyItems();
        }

        private void Update() {
            if (_breedsTemp != null) {
                DestroyItems();
                CreateItems();
                _breedsTemp = null;
            }
        }

        private void CreateItems() {
            for (int index = 0; index < _breedsTemp.Length; index++) {
                DogBreed breed = _breedsTemp[index];
                var breedInstance = Instantiate(_breedPrefab, _itemsParent);
                breedInstance.Set(index, breed, _signalBus);
            }
        }

        private void DestroyItems() {
            foreach (RectTransform child in _itemsParent) {
                Destroy(child.gameObject);
            }
        }
    }
}