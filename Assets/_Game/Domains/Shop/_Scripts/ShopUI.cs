using System;
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


        private Profile _profile;
        private IElementSpawner _elementSpawner;
        private ISaveManager _saveManager;

        private void Awake()
        {
            foreach (var element in _elementsList.Elements)
            {
                var card = Instantiate(_elementCardPfb, _cardsContainer, false);
                card.Setup(element, this);
            }
        }

        [Inject]
        public void Init(Profile profile, IElementSpawner elementSpawner, ISaveManager saveManager)
        {
            _profile = profile;
            _elementSpawner = elementSpawner;
            _saveManager = saveManager;
        }

        public void OnCardClicked(ElementSO element)
        {
            var price = element.Price;

            if (_profile.ResourcesInventory.CanSubtract(price.Resource, price.Amount))
            {
                _elementSpawner.SpawnNewAndPlace(element.Element.Pfb, null,
                    () => OnPlacedSuccessfully(price));
                _panelActivator.ForceHide();
            }

        }

        private void OnPlacedSuccessfully(ResourcePrice price)
        {
            _profile.ResourcesInventory.SubtratResource(price.Resource, price.Amount);
            _saveManager.Save(_profile.ResourcesInventory);
        }
    }
}