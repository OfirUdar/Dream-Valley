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
        public void Setup(ElementSO shopElement,ShopUI shop)
        {
            _shop = shop;
            _shopElement = shopElement;

            _titleText.text = shopElement.Element.Data.Name;
            _mainImage.sprite = shopElement.Element.Data.Sprite;

            _priceText.text = shopElement.Price.Amount.ToString();
            _resourceImage.sprite = shopElement.Price.Resource.Sprite;
        }

        public void OnElementClicked()
        {
            _shop.StartPlace(_shopElement);
        }
        
    }
}