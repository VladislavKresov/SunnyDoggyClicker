using UnityEngine;

namespace SunnyDoggyClicker.UI {
    public class DogsView : View {
        public override string Name => nameof(DogsView);

        protected override void OnHide() {
            Debug.Log($"{Name}::OnHide()");
        }

        protected override void OnShow() {
            Debug.Log($"{Name}::OnShow()");
        }
    }
}