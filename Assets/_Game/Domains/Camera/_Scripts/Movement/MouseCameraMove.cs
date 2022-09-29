using UnityEngine;

namespace Game.Camera
{
    public class MouseCameraMove : CameraMoveBase
    {

        private bool _isDragging;
        private Vector3 _dragOrigin;

        public MouseCameraMove(Transform camTran, IUserInput input, MoveSettings moveSettings) : base(camTran, input, moveSettings)
        {
        }

        public override void Tick()
        {
            if (_input.IsPointerDown() && !_input.IsPointerOverUI())
            {
                _dragOrigin = GetWorldPointerPosition();
                _isDragging = true;
            }

            if (_isDragging&&_input.IsPointerPressing())
            {
                var currentPosition = GetWorldPointerPosition();

                var delta = _dragOrigin - currentPosition;

                var nextPos = _camTran.position + delta;
                nextPos = ConvertToValidPosition(nextPos);

                Move(nextPos);

                _dragOrigin = currentPosition;
            }

            if (_input.IsPointerUp())
                _isDragging = false;

        }

    }

}

