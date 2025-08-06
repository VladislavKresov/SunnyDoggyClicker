using UnityEditor;
using UnityEngine;
using Zenject;

namespace SunnyDoggyClicker.UI.Navigation {
    public class BottomNavigationView : MonoBehaviour {
        [SerializeField] private NavigationButtonView _navigationViewPrefab;
        [SerializeField] private RectTransform _buttonsParent;

        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus) {
            _signalBus = signalBus;
        }

        public NavigationButtonDescription[] NavigationButtonDescriptions;
#if UNITY_EDITOR
        private void OnValidate() {
            if (!EditorApplication.isPlayingOrWillChangePlaymode) {
                CreateButtons();
            }
        }
#endif
        private void Start() {
            CreateButtons();
        }

        [ExecuteAlways]
        private void CreateButtons() {
            if (_buttonsParent != null) {
                foreach (RectTransform child in _buttonsParent) {
#if UNITY_EDITOR
                    if (!EditorApplication.isPlayingOrWillChangePlaymode) {
                        EditorApplication.delayCall += () => DestroyImmediate(child.gameObject, false);
                    }
                    else {
                        Destroy(child.gameObject);
                    }
#else
                Destroy(child.gameObject);
#endif
                }
                foreach (var buttonDescription in NavigationButtonDescriptions) {
                    if (buttonDescription == null || _navigationViewPrefab == null) {
                        continue;
                    }
                    var prefabInstance = Instantiate(_navigationViewPrefab, _buttonsParent);
                    prefabInstance.SetUpByButtonDescription(buttonDescription);
                    prefabInstance.OnClickAction = ShowScreen;
                }
            }
        }

        private void ShowScreen(string screenName) {
            _signalBus.Fire(new ShowViewSignal() {ViewName = screenName});
        }
    }
}