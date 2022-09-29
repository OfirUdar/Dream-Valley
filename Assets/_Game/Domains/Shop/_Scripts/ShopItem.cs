using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Shop.UI
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private Image _mainImage;
        [Header("Price")]
        [SerializeField] private Image _resourcePriceImage;
        [SerializeField] private TextMeshProUGUI _priceText;

        private ShopManager _shopManager;
        private PlacementSO _placement;


        public void Setup(ShopManager manager, PlacementSO placement)
        {
            _shopManager = manager;
            _placement = placement;

            var data = placement.Data;

            _titleText.text = data.Name;
            _mainImage.sprite = data.Sprite;

            _priceText.text = data.ResourcePrice.Price.ToString();
            _resourcePriceImage.sprite = data.ResourcePrice.Resource.Sprite;

        }

        public void Select()
        {
            _shopManager.CreatePlacement(_placement);
        }
    }
}

