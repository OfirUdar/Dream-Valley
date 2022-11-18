using UnityEngine;
using Zenject;

namespace Game.Shop.UI
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private PanelActivator _panelActivator;
        [Space]
        [SerializeField] private ElementListSO _elementsList;
        [SerializeField] private ElementCardUI _elementCardPfb;
        [Space]
        [SerializeField] private Transform _cardsContainer;
        private IElementSpawner _elementSpawner;

        private void Awake()
        {
            foreach (var element in _elementsList.Elements)
            {
                var card = Instantiate(_elementCardPfb, _cardsContainer, false);
                card.Setup(element,this);
            }
        }

        [Inject]
        public void Init(IElementSpawner elementSpawner)
        {
            _elementSpawner = elementSpawner;
        }

        public void StartPlace(ElementSO element)
        {
            _elementSpawner.Spawn(element.Element.Pfb);
            _panelActivator.ForceHide();
        }
    }
}