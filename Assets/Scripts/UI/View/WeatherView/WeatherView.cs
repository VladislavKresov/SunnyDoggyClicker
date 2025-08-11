using UnityEngine;

namespace SunnyDoggyClicker.UI.View.Weather {
    public class WeatherView : View {
        public override string Name => nameof(WeatherView);

        protected override void OnHide() {
            Debug.Log($"{Name}::OnHide()");
        }

        protected override void OnShow() {
            Debug.Log($"{Name}::OnShow()");
        }
    }
}