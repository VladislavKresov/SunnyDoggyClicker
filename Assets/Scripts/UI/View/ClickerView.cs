using UnityEngine;

namespace SunnyDoggyClicker.UI {
    public class ClickerView : View {
        public override string Name => nameof(ClickerView);

        protected override void OnHide() {
            Debug.Log($"{Name}::OnHide()");
        }

        protected override void OnShow() {
            Debug.Log($"{Name}::OnShow()");
        }
    }
}