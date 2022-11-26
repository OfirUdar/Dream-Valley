using System;
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

        private Profile _profile;
        private IElementSpawner _elementSpawner;
        private ISaveManager _saveManager;
      
        [Inject]
        public void Init(Profile profile, IElementSpawner elementSpawner, ISaveManager saveManager)
        {
            _profile = profile;
            _elementSpawner = elementSpawner;
            _saveManager = saveManager;

            foreach (var element in _elementsList.Elements)
            {
                var card = Instantiate(_elementCardPfb, _cardsContainer, false);
                card.Setup(element, this, CanPurchase(element.Price));
                _elementsDictionaryUI.Add(card, element.Price);
            }

            _profile.ResourcesInventory.ResourceChanged += OnResourcesChanged;
        }
        private void OnDestroy()
        {
            _profile.ResourcesInventory.ResourceChanged -= OnResourcesChanged;
        }

        private bool CanPurchase(ResourcePrice price)
        {
            return _profile.ResourcesInventory.CanSubtract(price.Resource, price.Amount);
        }

        public void OnCardClicked(ElementSO element)
        {
            var price = element.Price;

            if (CanPurchase(price))
            {
                _elementSpawner.SpawnNewAndPlace(element.Element.Pfb, null,
                    () => OnPlacedSuccessfully(price));
                _panelActivator.ForceHide();
            }
            else
                DialogManager.Instance.Show("Error", "Not enough resources!");

        }

        private void OnPlacedSuccessfully(ResourcePrice price)
        {
            _profile.ResourcesInventory.SubtratResource(price.Resource, price.Amount);
            _saveManager.Save(_profile.ResourcesInventory);
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