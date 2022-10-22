using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Game
{
    public class DragTrigger : MonoBehaviour, IDraggable
    {
        private DragEventAggragator _dragEventAggragator;
        private PlacementFacade _placementFacade;

        public UnityEvent DragStarted;
        public UnityEvent DragEnded;
        public UnityEvent<bool> Dragging; // bool - can place?

        [Inject]
        public void Init(DragEventAggragator eventAggragator, PlacementFacade placementFacade)
        {
            _dragEventAggragator = eventAggragator;
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
            _dragEventAggragator.RequestStartDrag(_placementFacade);
        }

        private void OnMouseDrag()
        {
            _dragEventAggragator.RequestDrag();
        }
        private void OnMouseUp()
        {
            _dragEventAggragator.RequestEndDrag();
        }
    }
}