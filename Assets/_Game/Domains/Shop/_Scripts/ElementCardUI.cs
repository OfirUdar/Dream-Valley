using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Shop.UI
{
    public class ElementCardUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private Image _mainImage;
        [Header("Price")]
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Image _resourceImage;

        private ShopUI _shop;
        private ElementSO _shopElement;

        public void Setup(ElementSO shopElement, ShopUI shop, bool canPurchase)
        {
            _shop = shop;
            _shopElement = shopElement;

            _titleText.text = shopElement.Element.Data.Name;
            _mainImage.sprite = shopElement.Element.Data.Sprite;

            _resourceImage.sprite = shopElement.Price.Resource.Sprite;

            SetAvailableForPurchase(canPurchase);
        }
        public void SetAvailableForPurchase(bool canPurchase)
        {
            var priceText = _shopElement.Price.Amount.ToString();
            _priceText.text = canPurchase ? priceText : "<color=red>" + priceText + "</color>";
        }
        public void OnElementClicked()
        {
            _shop.OnCardClicked(_shopElement);
        }

    }
}