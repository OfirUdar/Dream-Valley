using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Game
{
    public class DragTrigger : MonoBehaviour, IDraggable
    {
        private IDragController _dragController;
        private PlacementFacade _placementFacade;

        public UnityEvent DragStarted;
        public UnityEvent DragEnded;
        public UnityEvent<bool> Dragging; // bool - can place?

        [Inject]
        public void Init(IDragController controller, PlacementFacade placementFacade)
        {
            _dragController = controller;
            _placementFacade = placementFacade;
        }
        public void StartDrag()
        {
            DragStarted?.Invoke();
        }
        public void EndDrag()
        {
            DragEnded?.Invoke();
        }
        public void OnDrag(bool canPlace)
        {
            Dragging?.Invoke(canPlace);
        }

        private void OnMouseDown()
        {
            _dragController.RequestStartDrag(_placementFacade);
        }

        private void OnMouseDrag()
        {
            _dragController.RequestDrag();
        }
        private void OnMouseUp()
        {
            _dragController.RequestEndDrag();
        }
    }
}