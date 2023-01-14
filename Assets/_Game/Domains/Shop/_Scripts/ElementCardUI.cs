using TMPro;
using Udar;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Shop.UI
{
    public class ElementCardUI : MonoBehaviour
    {
        [SerializeField] private UdarCanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private Image _mainImage;
        [Header("Price")]
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Image _resourceImage;

        private ShopUI _shop;
        private ElementSO _shopElement;

        public ElementCardUI Setup(ElementSO shopElement, ShopUI shop)
        {
            _shop = shop;
            _shopElement = shopElement;

            _titleText.text = shopElement.Element.Name;
            _mainImage.sprite = shopElement.Element.Sprite;

            _resourceImage.sprite = shopElement.Price.Resource.Sprite;


            return this;
        }
        public ElementCardUI SetAvailableForPurchase(bool canPurchase)
        {
            var priceText = _shopElement.Price.Amount.ToString();
            _priceText.text = canPurchase ? priceText : "<color=red>" + priceText + "</color>";

            return this;
        }
        public ElementCardUI SetAmount(int current, int max)
        {
            _amountText.text = current + " / " + max;

            if (current == max)
                _canvasGroup.Activate(false);

            return this;
        }
        public void OnElementClicked()
        {
            _shop.OnCardClicked(_shopElement);
        }

    }
}