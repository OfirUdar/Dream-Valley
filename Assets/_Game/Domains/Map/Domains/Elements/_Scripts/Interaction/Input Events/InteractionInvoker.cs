using Zenject;

namespace Game.Map.Element
{
    public class InteractionInvoker : IInitializable, ILateDisposable
    {

        private readonly IMapElement _mapElement;
        private readonly PointerEvents _pointerEvents;
        private readonly ISelectionManager _selectionManager;
        private readonly IDragManager _dragManager;

        private readonly IUserInput _input;

        private readonly bool _isDraggable;

        public InteractionInvoker(IMapElement mapElement,
            PointerEvents pointerEvents,
            ISelectionManager selectionManager,
            IDragManager dragManager,
            IUserInput input,
            bool isDraggable = true)
        {
            _mapElement = mapElement;
            _pointerEvents = pointerEvents;
            _selectionManager = selectionManager;
            _dragManager = dragManager;

            _input = input;

            _isDraggable = isDraggable;
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
            if (_input.IsPointerOverUI())
                return;

            _selectionManager.RequestSelect(_mapElement);
        }


        private void OnPointerStartDrag()
        {
            if (!_isDraggable || _input.IsPointerOverUI())
                return;

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