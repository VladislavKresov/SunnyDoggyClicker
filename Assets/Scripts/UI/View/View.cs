using UnityEngine;

namespace SunnyDoggyClicker.UI.View {
    public abstract class View : MonoBehaviour {
        public abstract string Name { get; }
        private bool _isShown = false;
        public bool IsShown { get { return _isShown; } }

        public void Show() {
            gameObject.SetActive(true);
            _isShown = true;
            OnShow();
        }

        protected abstract void OnShow();

        public void Hide() {
            gameObject.SetActive(false);
            _isShown = false;
            OnHide();
        }

        protected abstract void OnHide();
    }
}