using System.Collections;
using System.Collections.Generic;
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
        public void Spawn(GameObject gameObject, bool isNew)
        {
            var startPosition = _camPointerUtility.CameraRaycast();
            var facadeBehaviour = _factory.Create(gameObject);
            facadeBehaviour.transform.position = startPosition;


            if (isNew)
            {
                var mapElement = facadeBehaviour.MapElement;
                mapElement.StartDrag();
                mapElement.PlaceApprover.Show();
                mapElement.PlaceApprover.SubscribeForCallbacks(
                      ()=> OnAprroved(mapElement)
                    , () => OnCanceled(mapElement));

                _selectionManager.RequestSelect(mapElement);
                _selectionManager.Lock(true);
                _dragManager.ChangeToNewElementDragger();
                MainUIEventAggregator.Hide();
                _cameraController.FocusAsync(startPosition, 15f);
            }

        }

        private void OnAprroved(IMapElement mapElement)
        {
            mapElement.PlaceApprover.Hide();

            _selectionManager.Lock(false);
            _dragManager.ChangeToExistElementDragger();
            MainUIEventAggregator.Show();
        }
        private void OnCanceled(IMapElement mapElement)
        {
            mapElement.Destroy();

            _selectionManager.Lock(false);
            _dragManager.ChangeToExistElementDragger();
            MainUIEventAggregator.Show();
            
        }

    }
}

