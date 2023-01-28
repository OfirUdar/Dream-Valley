using UnityEngine;
using Zenject;

namespace Game.Map
{
    public class ExistDragState : DraggerStateBase
    {
        [Inject] private readonly IVFXFactory _vfxFactory;

        private Vector3 _startPosition;
        public ExistDragState(IMapGrid grid, ICameraController cameraController, ICameraPointerUtility camPointerUtility, ISelectionManager selectionManager) : base(grid, cameraController, camPointerUtility, selectionManager)
        {
        }

        public override void OnDragStarted()
        {
            _startPosition = _currentElement.Position;
        }

        public override void OnDragEnded(bool canPlace)
        {
            _currentElement.EndDrag(canPlace);

            if (canPlace)
            {
                if (_currentElement.Position != _startPosition)
                {
                    _grid.Remove(_startPosition, _currentElement.Width, _currentElement.Height); //removing from the old
                    _grid.Place(_currentElement);
                    _vfxFactory.CreateEffect(VFXType.ElementPlaced, _currentElement.Center);
                }

                _soundsManager.PlayOneShot(_dragSounds.PlacedAudioInfo);
                _currentElement = null;
            }
            else
                _soundsManager.PlayOneShot(_dragSounds.ErrorPlacedAudioInfo);

        }
        public override void OnCanceled()
        {
            _currentElement.Position = _startPosition;
            _currentElement.EndDrag(true);
        }

        protected override bool CanStartDrag(IMapElement mapElement)
        {
            return mapElement.IsSelected;
        }
    }

}