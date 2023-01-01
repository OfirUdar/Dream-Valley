using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Map.Element
{
    public class UIDataRow : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;

        public UIDataRow SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;

            return this;
        }
        public UIDataRow SetText(string text)
        {
            _text.text = text;

            return this;
        }

    }

}
