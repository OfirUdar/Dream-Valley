using Zenject;

namespace Game.Map.Element
{
    public class InteractionCaller : IInitializable, ILateDisposable
    {
        private readonly IMapElement _mapElement;
        private readonly PointerEvents _pointerEvents;
        private readonly SelectionManager _selectionManager;
        private readonly DragManager _dragManager;

        public InteractionCaller(IMapElement mapElement, PointerEvents pointerEvents,
            SelectionManager selectionManager, DragManager dragManager)
        {
            _mapElement = mapElement;
            _pointerEvents = pointerEvents;
            _selectionManager = selectionManager;
            _dragManager = dragManager;
        }

        public void Initialize()
        {
            _pointerEvents.UpAsButton += OnPoinerUpAsButton;

            _pointerEvents.StartDrag += OnPointerStartDrag;
            _pointerEvents.EndDrag += OnPointerEndDrag;
            _pointerEvents.Dragging += OnPointerDragging;
        }

        public void LateDispose()
        {
            _pointerEvents.UpAsButton -= OnPoinerUpAsButton;

            _pointerEvents.StartDrag -= OnPointerStartDrag;
            _pointerEvents.EndDrag -= OnPointerEndDrag;
            _pointerEvents.Dragging -= OnPointerDragging;
        }

        private void OnPoinerUpAsButton()
        {
            _selectionManager.RequestSelect(_mapElement);
        }

     
        private void OnPointerStartDrag()
        {
            _dragManager.RequestStartDrag(_mapElement);
        }
        private void OnPointerEndDrag()
        {
            _dragManager.RequestEndDrag();
        }
        private void OnPointerDragging()
        {
            _dragManager.RequestDrag();
        }

    }
}