using Udar;
using UnityEngine;
using Zenject;

namespace Game
{
    public class MouseCameraMove : ITickable
    {
        private readonly Transform _camTran;
        private readonly IUserInput _input;
        private readonly MoveSettings _settings;

        private Vector3 _lastPosition;
        public MouseCameraMove(Transform camTran, IUserInput input,MoveSettings moveSettings)
        {
            _camTran = camTran;
            _input = input;
            _settings = moveSettings;
        }

        public void Tick()
        {
            if (_input.IsPointerDown())
            {
                _lastPosition = CameraUtils.Cam
                    .ScreenToWorldPoint(_input.GetPointerPosition());
            }

            if (_input.IsPointerPressing())
            {
                var currentPosition = CameraUtils.Cam
                    .ScreenToWorldPoint(_input.GetPointerPosition());

                var diff = currentPosition - _lastPosition;
                diff.z = 0;
                var nextPos = _camTran.localPosition - diff;

                Move(nextPos);

                 _lastPosition = currentPosition;
            }

            
        }

        private void Move(Vector3 nextPos)
        {
            nextPos.x = Mathf.Clamp(nextPos.x, _settings.HorizontalLimits.x, _settings.HorizontalLimits.y);
            nextPos.y = Mathf.Clamp(nextPos.y, _settings.VerticalLimits.x, _settings.VerticalLimits.y);

            _camTran.localPosition = Vector3.Lerp(_camTran.localPosition, nextPos, .8f);
        }
    }

}

