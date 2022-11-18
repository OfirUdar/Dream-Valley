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

        public ElementSpawner(SelectionManager selectionManager,
            IDragManager dragManager, CamPointerUtility camPointerUtility,
            FacadeBehaviour.Factory factory)
        {
            _selectionManager = selectionManager;
            _dragManager = dragManager;
            _camPointerUtility = camPointerUtility;
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
                _selectionManager.RequestSelect(mapElement);
                _dragManager.ChangeToNewElementDragger();
            }

        }

    }
}

