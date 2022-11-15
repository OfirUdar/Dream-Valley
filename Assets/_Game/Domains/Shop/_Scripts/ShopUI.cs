using UnityEngine;

namespace Game.Shop.UI
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private ElementListSO _elementsList;
        [SerializeField] private ElementCardUI _elementCardPfb;
        [Space]
        [SerializeField] private Transform _cardsContainer;

        private void Awake()
        {
            foreach (var element in _elementsList.Elements)
            {
                var card = Instantiate(_elementCardPfb, _cardsContainer, false);
                card.Setup(element);
            }
        }
    }
}