using Udar;
using UnityEngine;
using Zenject;

namespace Game.Camera
{
    public class TouchCameraMove : ITickable
    {
        private readonly Transform _camTran;
        private readonly IUserInput _input;
        private readonly MoveSettings _settings;

        private Plane _plane;
        public TouchCameraMove(Transform camTran, IUserInput input, MoveSettings moveSettings)
        {
            _camTran = camTran;
            _input = input;
            _settings = moveSettings;

            _plane = new Plane(Vector3.forward, Vector3.zero);
        }

        public void Tick()
        {
            if (Input.touchCount >= 1)
            {
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    var delta = PlanePositionDelta(touch);
                    delta.z = 0;
                    Move(delta + _camTran.localPosition);
                }
            }


        }
        protected Vector3 PlanePositionDelta(Touch touch)
        {
            //not moved
            if (touch.phase != TouchPhase.Moved)
                return Vector3.zero;

            //delta
            var rayBefore = CameraUtils.Cam.ScreenPointToRay(touch.position - touch.deltaPosition);
            var rayNow = CameraUtils.Cam.ScreenPointToRay(touch.position);
            if (_plane.Raycast(rayBefore, out var enterBefore) && _plane.Raycast(rayNow, out var enterNow))
                return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);

            //not on plane
            return Vector3.zero;
        }
        protected Vector3 PlanePositionDelta2(Touch touch)
        {
            //not moved
            if (touch.phase != TouchPhase.Moved)
                return Vector3.zero;

            //delta
            var beforeTouchWorldPos = CameraUtils.Cam.ScreenToWorldPoint(touch.position - touch.deltaPosition);
            var currentTouchWorldPos = CameraUtils.Cam.ScreenToWorldPoint(touch.position);

            return beforeTouchWorldPos - currentTouchWorldPos;
        }
        private void Move(Vector3 nextPos)
        {
            nextPos.x = Mathf.Clamp(nextPos.x, _settings.HorizontalLimits.x, _settings.HorizontalLimits.y);
            nextPos.y = Mathf.Clamp(nextPos.y, _settings.VerticalLimits.x, _settings.VerticalLimits.y);

            _camTran.localPosition = Vector3.Lerp(_camTran.localPosition, nextPos, .5f);
        }
    }

}

