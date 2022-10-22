using System.Collections;
using System.Threading.Tasks;
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
        private ScenesCollection _scenesCollection;
        private SelectionEventAggragator _selectionEventAggragator;
        private IGrid _grid;

        //private ISelectionManager _selectionManager;

        private PlacementBehaviour _placementInstance;


        private void Awake()
        {
            foreach (var placement in _placementList.PlacementsList)
            {
                var shopItemInstance = Instantiate(_shopItemPfb, _container, false);
                shopItemInstance.Setup(this, placement);
            }
        }

        [Inject]
        public void Init(PlacementBehaviour.Factory placementFactory,
            ScenesCollection scenesCollection,
            SelectionEventAggragator selectionEventAggragator,
            IGrid grid)
        {
            _placementFactory = placementFactory;
            _scenesCollection = scenesCollection;
            _selectionEventAggragator = selectionEventAggragator;
            _grid = grid;
        }

        public async void CreatePlacement(PlacementSO placement)
        {
            Debug.Log("start " + Time.time);

            await ChangeToPurchaseState();
            _placementInstance = _placementFactory.Create(placement.Pfb);
            var facade = _placementInstance.Facade;

            Debug.Log("unloaded " + Time.time);
            _selectionEventAggragator.RequestSelect(facade.Selectable);
            facade.Draggable.StartDrag();
            facade.PlaceApprover.SubscribeForCallbacks(ApprovePlacement, CancelPlacement);
            facade.PlaceApprover.Show();
            gameObject.SetActive(false);
        }

        public void ApprovePlacement()
        {
            //Purchase
            _grid.Place(_placementInstance);
            _placementInstance.Facade.PlaceApprover.Hide();
            _placementInstance.Facade.Draggable.EndDrag();
            ChangeToIdleEditState();
        }
        public void CancelPlacement()
        {
            Destroy(_placementInstance.gameObject);
            ChangeToIdleEditState();
        }


        private async Task ChangeToPurchaseState()
        {
             await _scenesCollection.Unload(_scenesCollection.IdleEditState.SceneName);
            _scenesCollection.LoadAddtive(_scenesCollection.PurchaseEditState.SceneName);
            await Task.Yield();
        }
        private async void ChangeToIdleEditState()
        {
            await _scenesCollection.Unload(_scenesCollection.PurchaseEditState.SceneName);
            _scenesCollection.LoadAddtive(_scenesCollection.IdleEditState.SceneName);
        }


    }
}