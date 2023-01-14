using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Map.Element.Building.TownHall
{
    public class UIElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _amountRemainText;
        [SerializeField] private Image _elementImage;


        public UIElement SetAmountRemain(int amount)
        {
            _amountRemainText.text = "x" + amount;

            return this;
        }
        public UIElement SetSprite(Sprite sprite)
        {
            _elementImage.sprite = sprite;

            return this;
        }


    }
}