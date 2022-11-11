using Udar;
using UnityEngine;

namespace Game.Camera
{
    public class TouchCameraMove : CameraMoveBase
    {
        private const float STOP_PAN_VELOCITY = 0.008f;
        private Vector3 _panVelocity;
        private bool _startInertia;

        public TouchCameraMove(Transform camTran, IUserInput input, MoveSettings moveSettings) : base(camTran, input, moveSettings)
        {
        }

        public override void Tick()
        {
            if (Input.touchCount >= 1 && !_input.IsPointerOverUI())
            {
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    var delta = GetTouchDelta(touch);
                    var nextPos = _camTran.position + delta;
                    nextPos = ConvertToValidPosition(nextPos);
                    Move(nextPos);

                    _panVelocity = delta;
                }
                else if (touch.phase == TouchPhase.Stationary)
                    _panVelocity = Vector3.zero;
            }
            if (_input.IsPointerUp())
            {
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
        protected Vector3 GetTouchDelta(Touch touch)
        {
            //not moved
            if (touch.phase != TouchPhase.Moved)
                return Vector3.zero;

            //delta
            var beforeTouchWorldPos = CameraUtils.Main.ScreenToWorldPoint(touch.position - touch.deltaPosition);
            var currentTouchWorldPos = CameraUtils.Main.ScreenToWorldPoint(touch.position);

            return beforeTouchWorldPos - currentTouchWorldPos;
        }

    }

}

