using System;
using UnityEngine;

namespace Game.Map.Element
{
    public class ElementSpawner : IElementSpawner
    {
        private readonly FacadeBehaviour.Factory _factory;
        private readonly SelectionManager _selectionManager;
        private readonly IDragManager _dragManager;
        private readonly CamPointerUtility _camPointerUtility;
        private readonly ICameraController _cameraController;

        public ElementSpawner(SelectionManager selectionManager,
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
        public void Spawn(GameObject gameObject)
        {
            var startPosition = _camPointerUtility.CameraRaycast();
            var facadeBehaviour = _factory.Create(gameObject);
            facadeBehaviour.transform.position = startPosition;
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
                  () => OnAprroved(mapElement, successCallback)
                , () => OnCanceled(mapElement, cancelCallback));

            _selectionManager.RequestSelect(mapElement);
            _selectionManager.Lock(true);
            _dragManager.ChangeToNewElementDragger();
            _cameraController.FocusAsync(startPosition, 15f);
            MainUIEventAggregator.Hide();
        }

        private void OnAprroved(IMapElement mapElement,Action successCallback)
        {
            mapElement.PlaceApprover.Hide();

            _selectionManager.Lock(false);
            _dragManager.ChangeToExistElementDragger();
            MainUIEventAggregator.Show();

            successCallback?.Invoke();
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

