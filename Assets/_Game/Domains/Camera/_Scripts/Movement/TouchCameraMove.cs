using Udar;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Camera
{
    public class TouchCameraMove : CameraMoveBase
    {
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
                }
            }

        }
        protected Vector3 GetTouchDelta(Touch touch)
        {
            //not moved
            if (touch.phase != TouchPhase.Moved)
                return Vector3.zero;

            //delta
            var beforeTouchWorldPos = CameraUtils.Cam.ScreenToWorldPoint(touch.position - touch.deltaPosition);
            var currentTouchWorldPos = CameraUtils.Cam.ScreenToWorldPoint(touch.position);

            return beforeTouchWorldPos - currentTouchWorldPos;
        }

    }

}

