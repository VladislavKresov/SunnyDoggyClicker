using UnityEngine;

namespace SunnyDoggyClicker.UI.Navigation {
    [CreateAssetMenu(fileName = "NavigationButton", menuName = "SunnyDoggyClicker/UI/Bottom Navigation Button Description")]
    public class NavigationButtonDescription : ScriptableObject {
        public string ScreenName;
        [Space]
        public Sprite BackgroundImage;
        public Color BackgroundImageColor = Color.white;
        [Space]
        public Sprite ForegroundImage;
        public Color ForegroundImageColor = Color.white;
    }
}