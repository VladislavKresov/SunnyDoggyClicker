using UnityEngine;
using UnityEngine.UI;

namespace SunnyDoggyClicker.UI.Navigation {
    public class NavigationButtonView : MonoBehaviour {
        [SerializeField] private Button _showViewBTN;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _foregroundImage;
        public delegate void OnNavigationButtonClick(string screenName);
        public OnNavigationButtonClick OnClickAction;
        private string _targetScreenName;

        public void SetUpByButtonDescription(NavigationButtonDescription navigationButtonDescription) {
            if (_showViewBTN != null && !string.IsNullOrEmpty(navigationButtonDescription.ScreenName)) {
                _targetScreenName = navigationButtonDescription.ScreenName;
                gameObject.name = _targetScreenName + "_NavigationBTN";
                _showViewBTN.onClick.AddListener(() => OnClickAction?.Invoke(_targetScreenName));
            }
            if (_backgroundImage != null) {
                _backgroundImage.sprite = navigationButtonDescription.BackgroundImage;
                _backgroundImage.color = navigationButtonDescription.BackgroundImageColor;
            }
            if (_foregroundImage != null) {
                _foregroundImage.sprite = navigationButtonDescription.ForegroundImage;
                _foregroundImage.color = navigationButtonDescription.ForegroundImageColor;
            }
        }
    }
}