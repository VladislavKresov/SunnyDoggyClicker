using UnityEngine;

namespace SunnyDoggyClicker.UI.View {
    public abstract class View : MonoBehaviour {
        public abstract string Name { get; }

        public void Show() {
            gameObject.SetActive(true);
            OnShow();
        }

        public void Hide() {
            gameObject.SetActive(false);
            OnHide();
        }

        protected virtual void OnAwake() { }
        protected virtual void OnShow() { }
        protected virtual void OnHide() { }

        private void Awake() {
            OnAwake();
        }
    }

    public abstract class View<TViewModel> : View where TViewModel : class {
        protected TViewModel ViewModel { get; private set; }

        public virtual void Bind(TViewModel viewModel) {
            ViewModel = viewModel;
            OnBind();
        }

        protected virtual void OnBind() { }
    }
}