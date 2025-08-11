using UnityEngine;
using UnityEngine.UI;

namespace SunnyDoggyClicker.UI.Elements {
    public class ImageWithText : MonoBehaviour {
        [SerializeField] protected Image image;
        [SerializeField] protected Text text;

        public virtual void SetSprite(Sprite sprite) {
            if (image == null){
                return;
            }
            image.sprite = sprite;
        }

        public virtual void SetImageColor(Color color) {
            if (image == null) {
                return;
            }
            image.color = color;
        }

        public virtual void SetText(string text) {
            if (text == null) {
                return;
            }
            this.text.text = text;
        }

        public virtual void SetTextColor(Color color) {
            if (text == null) {
                return;
            }
            this.text.color = color;
        }
    }
}