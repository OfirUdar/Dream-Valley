using System.Collections;
using UnityEngine;
using Zenject;

namespace Game.Shop.UI
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private PlacementListSO _placementList;
        [SerializeField] private RectTransform _container;
        [SerializeField] private ShopItem _shopItemPfb;

        private PlacementBehaviour.Factory _placementFactory;


        private void Awake()
        {
            foreach (var placement in _placementList.PlacementsList)
            {
                var shopItemInstance = Instantiate(_shopItemPfb, _container, false);
                shopItemInstance.Setup(this, placement);
            }
        }

        [Inject]
        public void Init(PlacementBehaviour.Factory placementFactory)
        {
            _placementFactory = placementFactory;
        }

        public void CreatePlacement(PlacementSO placement)
        {
            var placementInstance = _placementFactory.Create(placement.Pfb);
            var selectable = placementInstance.GetComponentInChildren<ISelectable>();
            selectable.Select();
            gameObject.SetActive(false);
        }

    }
}