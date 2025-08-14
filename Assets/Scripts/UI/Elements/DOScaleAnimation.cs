using DG.Tweening;
using UnityEngine;

namespace SunnyDoggyClicker.UI.Elements {
    public class DOScaleAnimation : MonoBehaviour {
        public float BounceScale = 1.2f;
        public float Duration = 0.2f;

        private RectTransform _rectTransform;

        public void Play() {
            if (_rectTransform == null) {
                _rectTransform = GetComponent<RectTransform>();
            }
            _rectTransform.DOScale(BounceScale, Duration).SetEase(Ease.OutBounce).OnComplete(() => _rectTransform.DOScale(1f, Duration / 2));
        }
    }
}