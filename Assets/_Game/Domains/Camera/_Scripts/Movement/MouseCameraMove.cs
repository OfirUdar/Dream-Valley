using UnityEngine;

namespace Game.Camera
{
    public class MouseCameraMove : CameraMoveBase
    {
        private const float STOP_PAN_VELOCITY = 0.0008f;

        private bool _isDragging;
        private Vector3 _dragOrigin;

        private Vector3 _panVelocity;
        private bool _startInertia;

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

            if (_isDragging && _input.IsPointerPressing())
            {
                var currentPosition = GetWorldPointerPosition();

                var delta = _dragOrigin - currentPosition;

                var nextPos = _camTran.position + delta;
                nextPos = ConvertToValidPosition(nextPos);
                Move(nextPos);

                _panVelocity = delta;

                _dragOrigin = currentPosition;
            }

            if (_input.IsPointerUp())
            {
                _isDragging = false;
                _startInertia = true;
            }

            if (_startInertia)
            {
                if (_panVelocity.sqrMagnitude < STOP_PAN_VELOCITY)
                    _startInertia = false;

                _panVelocity = Vector3.Lerp(_panVelocity, Vector3.zero, _settings.InertiaInterpolation);
                var nextPos = _camTran.position + _panVelocity;
                nextPos = ConvertToValidPosition(nextPos);
                Move(nextPos);
            }

        }

    }

}

