using System;
using UnityEngine;
using Zenject;

namespace SunnyDoggyClicker.UI {
    public abstract class View : MonoBehaviour {
        public abstract string Name { get; }

        public void Show() {
            gameObject.SetActive(true);
            OnShow();
        }

        protected abstract void OnShow();

        public void Hide() {
            gameObject.SetActive(false);
            OnHide();
        }

        protected abstract void OnHide();
    }
}