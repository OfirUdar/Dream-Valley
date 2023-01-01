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
        private ElementSpawnerAggragator _elementSpawnerAggragator;
        private ISaveManager _saveManager;

        [Inject] private IDialog _dialog;
      
        [Inject]
        public void Init(Profile profile, ElementSpawnerAggragator elementSpawner, ISaveManager saveManager)
        {
            _profile = profile;
            _elementSpawnerAggragator = elementSpawner;
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
                _elementSpawnerAggragator.SpawnNewAndPlace(element.Element.Pfb, null,
                    () => OnPlacedSuccessfully(price));
                _panelActivator.ForceHide();
            }
            else
                _dialog.Show("Error", "Not enough resources!");

        }

        private void OnPlacedSuccessfully(ResourcePrice price)
        {
            _profile.ResourcesInventory.SubtractResource(price.Resource, price.Amount);
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