using System;
using UnityEngine;
using Zenject;

namespace Game.Map.Element
{
    public class ElementSpawner : IElementSpawner, IInitializable, ILateDisposable
    {
        private readonly MapElementFactory _factory;
        private readonly IMapGrid _mapGrid;
        private readonly ISelectionManager _selectionManager;
        private readonly IDragManager _dragManager;
        private readonly ICameraController _cameraController;

        [Inject] private readonly ElementSpawnerAggragator _spawnerAggragator;

        public event Action<IMapElement> NewSpawned;

        public ElementSpawner(IMapGrid mapGrid, ISelectionManager selectionManager,
            IDragManager dragManager,
            ICameraController cameraController,
            MapElementFactory factory)
        {
            _mapGrid = mapGrid;
            _selectionManager = selectionManager;
            _dragManager = dragManager;
            _cameraController = cameraController;
            _factory = factory;
        }

        public void Initialize()
        {
            _spawnerAggragator.SpawnNewRequested += SpawnNewAndPlace;
        }
        public void LateDispose()
        {
            _spawnerAggragator.SpawnNewRequested -= SpawnNewAndPlace;
        }

        public IMapElement Spawn(GameObject gameObject)
        {
            var mapElement = _factory.Create(gameObject);

            return mapElement;
        }
        public IMapElement SpawnDefault(GameObject gameObject)
        {
            var mapElement = _factory.Create(gameObject);

            mapElement.Eventor.NotifiySpawnedSuccessfully();

            return mapElement;
        }

        public async void SpawnNewAndPlace(MapElementSO mapElementSO, Action cancelCallback, Action successCallback)
        {
            //_mapGrid.FindAvailablePlace(mapElementSO.Width, mapElementSO.Height, out Vector3 startPosition);
            _mapGrid.FindRandomAvailablePlace(mapElementSO.Width, mapElementSO.Height, out Vector3 startPosition);
            var mapElement = Spawn(mapElementSO.Pfb);
            mapElement.Position = startPosition;

            mapElement.StartDrag();
            mapElement.PlaceApprover.Show();
            mapElement.PlaceApprover.SubscribeForCallbacks(
                  () => OnAprroved(mapElement, successCallback)
                , () => OnCanceled(mapElement, cancelCallback));

            _selectionManager.RequestUnselect();
            _selectionManager.Lock(true);
            _dragManager.Lock(true);
            _dragManager.ChangeToNewElementDragger(mapElement);
            MainUIEventAggregator.Hide();
            await _cameraController.FocusAsync(startPosition, zoom: 15f);
            _dragManager.Lock(false);
        }

        private void OnAprroved(IMapElement mapElement, Action successCallback)
        {
            mapElement.PlaceApprover.Hide();

            _selectionManager.Lock(false);
            _dragManager.ChangeToExistElementDragger();
            MainUIEventAggregator.Show();

            successCallback?.Invoke();

            NewSpawned?.Invoke(mapElement);

            mapElement.Eventor.NotifiySpawnedSuccessfully();
        }
        private void OnCanceled(IMapElement mapElement, Action cancelCallback)
        {
            mapElement.Destroy();

            _selectionManager.Lock(false);
            _dragManager.ChangeToExistElementDragger();
            MainUIEventAggregator.Show();

            cancelCallback?.Invoke();
        }


    }
}

