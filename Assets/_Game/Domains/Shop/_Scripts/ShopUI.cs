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


        private readonly Dictionary<ElementCardUI, ElementSO> _elementsDictionaryUI
            = new Dictionary<ElementCardUI, ElementSO>();

        private IResourcesInventory _resourceInventory;
        private ElementSpawnerAggragator _elementSpawnerAggragator;

        [Inject] private readonly IDialog _dialog;
        [Inject] private readonly IAvailableElementsCounter _availableElementsCounter;

        [Inject]
        public void Init(IResourcesInventory resourceInventory, ElementSpawnerAggragator elementSpawner)
        {
            _resourceInventory = resourceInventory;
            _elementSpawnerAggragator = elementSpawner;

            _resourceInventory.ResourceChanged += OnResourcesChanged;
            _panelActivator.Activated += OnPanelActivated;

            foreach (var element in _elementsList.Elements)
            {
                var card = Instantiate(_elementCardPfb, _cardsContainer, false);

                card.Setup(element, this)
                    .SetAvailableForPurchase(CanPurchase(element.Price));
                _elementsDictionaryUI.Add(card, element);
            }
        }

        private void OnDestroy()
        {
            _resourceInventory.ResourceChanged -= OnResourcesChanged;
            _panelActivator.Activated -= OnPanelActivated;
        }


        private bool CanPurchase(ResourcePrice price)
        {
            return _resourceInventory.CanSubtract(price.Resource, price.Amount);
        }

        private void OnPanelActivated()
        {
            foreach (var element in _elementsDictionaryUI)
            {
                var currentAmount = _availableElementsCounter.GetCurrentAmountElements(element.Value.Element);
                var maxAmount = _availableElementsCounter.GetMaxAmountElement(element.Value.Element);
                element.Key
                    .SetAmount(currentAmount, maxAmount);
            }
        }
        public void OnCardClicked(ElementSO element)
        {
            var price = element.Price;

            if (CanPurchase(price))
            {
                _elementSpawnerAggragator.SpawnNewAndPlace(element.Element, null,
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
                var canPurchase = CanPurchase(_elementsDictionaryUI[uiElement].Price);
                uiElement.SetAvailableForPurchase(canPurchase);
            }
        }
    }
}