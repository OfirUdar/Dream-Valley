using System;
using UnityEngine;
using Zenject;

namespace Game.Map.Element
{
    public class ElementSpawner : IElementSpawner, IInitializable, ILateDisposable
    {
        private readonly FacadeBehaviour.Factory _factory;
        private readonly ISelectionManager _selectionManager;
        private readonly IDragManager _dragManager;
        private readonly CamPointerUtility _camPointerUtility;
        private readonly ICameraController _cameraController;

        [Inject] private readonly ElementSpawnerAggragator _spawnerAggragator;

        public event Action<IMapElement> NewSpawned;

        public ElementSpawner(ISelectionManager selectionManager,
            IDragManager dragManager,
            CamPointerUtility camPointerUtility,
            ICameraController cameraController,
            FacadeBehaviour.Factory factory)
        {
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
            var facadeBehaviour = _factory.Create(gameObject);
            facadeBehaviour.transform.position = startPosition;

            return facadeBehaviour.MapElement;
        }

        public void SpawnNewAndPlace(GameObject gameObject, Action cancelCallback, Action successCallback)
        {
            var startPosition = _camPointerUtility.CameraRaycast();
            var facadeBehaviour = _factory.Create(gameObject);
            facadeBehaviour.transform.position = startPosition;

            var mapElement = facadeBehaviour.MapElement;
            mapElement.StartDrag();
            mapElement.PlaceApprover.Show();
            mapElement.PlaceApprover.SubscribeForCallbacks(
                  () => OnAprroved(facadeBehaviour, successCallback)
                , () => OnCanceled(mapElement, cancelCallback));

            _selectionManager.RequestUnselect();
            _selectionManager.Lock(true);
            _dragManager.ChangeToNewElementDragger();
            _cameraController.FocusAsync(startPosition, 15f);
            MainUIEventAggregator.Hide();
        }

        private void OnAprroved(FacadeBehaviour facadeBehaviour, Action successCallback)
        {
            var mapElement = facadeBehaviour.MapElement;

            mapElement.PlaceApprover.Hide();

            _selectionManager.Lock(false);
            _dragManager.ChangeToExistElementDragger();
            MainUIEventAggregator.Show();

            successCallback?.Invoke();

            NewSpawned?.Invoke(mapElement);

            facadeBehaviour.Eventor.NotifiySpawnedSuccessfully();
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

