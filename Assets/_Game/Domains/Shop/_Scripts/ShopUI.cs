using System.Collections.Generic;
using Udar;
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

        [SerializeField] private Dictionary<ElementCardUI,ResourcePrice> _elementsDictionaryUI = new Dictionary<ElementCardUI, ResourcePrice>();

        private IResourcesInventory _resourceInventory;
        private ElementSpawnerAggragator _elementSpawnerAggragator;

        [Inject] private IDialog _dialog;
      
        [Inject]
        public void Init(IResourcesInventory resourceInventory, ElementSpawnerAggragator elementSpawner)
        {
            _resourceInventory = resourceInventory;
            _elementSpawnerAggragator = elementSpawner;

           
            _resourceInventory.ResourceChanged += OnResourcesChanged;
        }
        private void Start()
        {
            foreach (var element in _elementsList.Elements)
            {
                var card = Instantiate(_elementCardPfb, _cardsContainer, false);
                card.Setup(element, this, CanPurchase(element.Price));
                _elementsDictionaryUI.Add(card, element.Price);
            }
        }

        private void OnDestroy()
        {
            _resourceInventory.ResourceChanged -= OnResourcesChanged;
        }

      
        private bool CanPurchase(ResourcePrice price)
        {
            return _resourceInventory.CanSubtract(price.Resource, price.Amount);
        }

        public void OnCardClicked(ElementSO element)
        {
            var price = element.Price;

            if (CanPurchase(price))
            {
                _elementSpawnerAggragator.SpawnNewAndPlace(element.Element.Pfb, null,
                    () => OnPlacedSuccessfully(price));
                _panelActivator.ForceHide();
            }
            else
                _dialog.Show("Error", "Not enough resources!");

        }

        private void OnPlacedSuccessfully(ResourcePrice price)
        {
            _resourceInventory.SubtractResource(price.Resource, price.Amount);
        }


        private void OnResourcesChanged(ResourceDataSO resource, int amount)
        {
            foreach (var uiElement in _elementsDictionaryUI.Keys)
            {
                var canPurchase = CanPurchase(_elementsDictionaryUI[uiElement]);
                uiElement.SetAvailableForPurchase(canPurchase);
            }
        }
    }
}