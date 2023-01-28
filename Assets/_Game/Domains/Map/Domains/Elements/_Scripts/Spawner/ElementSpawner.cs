using System;
using UnityEngine;
using Zenject;

namespace Game.Map.Element
{
    public class ElementSpawner : IElementSpawner, IInitializable, ILateDisposable
    {
        //private readonly FacadeBehaviour.Factory _factory;
        private readonly MapElementFactory _factory;
        private readonly IMapGrid _mapGrid;
        private readonly ISelectionManager _selectionManager;
        private readonly IDragManager _dragManager;
        private readonly ICameraPointerUtility _camPointerUtility;
        private readonly ICameraController _cameraController;

        [Inject] private readonly ElementSpawnerAggragator _spawnerAggragator;

        public event Action<IMapElement> NewSpawned;

        public ElementSpawner(IMapGrid mapGrid, ISelectionManager selectionManager,
            IDragManager dragManager,
            ICameraPointerUtility camPointerUtility,
            ICameraController cameraController,
            MapElementFactory factory)
        {
            _mapGrid = mapGrid;
            _selectionManager = selectionManager;
            _dragManager = dragManager;
            _camPointerUtility = camPointerUtility;
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
            var startPosition = _camPointerUtility.CameraRaycast();
            var mapElement = _factory.Create(gameObject);
            mapElement.Position = startPosition;

            return mapElement;
        }
        public IMapElement SpawnDefault(GameObject gameObject)
        {
            var startPosition = _camPointerUtility.CameraRaycast();
            var mapElement = _factory.Create(gameObject);
            mapElement.Position = startPosition;

            mapElement.Eventor.NotifiySpawnedSuccessfully();

            return mapElement;
        }

        public async void SpawnNewAndPlace(MapElementSO mapElementSO, Action cancelCallback, Action successCallback)
        {
            //var startPosition = _camPointerUtility.CameraRaycast();
            _mapGrid.FindAvailablePlace(mapElementSO.Width, mapElementSO.Height, out Vector3 startPosition);
            var mapElement = _factory.Create(mapElementSO.Pfb);
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
            await _cameraController.FocusAsync(startPosition, 15f);
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

