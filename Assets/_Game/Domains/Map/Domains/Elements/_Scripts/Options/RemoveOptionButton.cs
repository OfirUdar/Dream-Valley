using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Map.Element
{
    public class RemoveOptionButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Image _priceResourceImage;

        public void Initalize(IMapElement mapElement)
        {
            var price = mapElement.RemoveHandler.Price;

            _priceText.text = price.Amount.ToString();
            _priceResourceImage.sprite = price.Resource.Sprite;
        }
        public void Execute(IMapElement mapElement)
        {
            mapElement.RemoveHandler.Remove();
        }
    }
}
