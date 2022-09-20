using Udar;
using UnityEngine;

namespace Game.Camera
{
    public class CameraMoveBase
    {
        protected readonly Transform _camTran;
        protected readonly IUserInput _input;
        private readonly MoveSettings _settings;

        public CameraMoveBase(Transform camTran, IUserInput input, MoveSettings moveSettings)
        {
            _camTran = camTran;
            _input = input;
            _settings = moveSettings;
        }

        protected Vector3 GetWorldPointerPosition()
        {
            var pointerPos = _input.GetPointerPosition();
            var position = CameraUtils.Cam.ScreenToWorldPoint(pointerPos);
            return position;
        }
        protected Vector3 ConvertToValidPosition(Vector3 position)
        {
            position.x = Mathf.Clamp(position.x, _settings.HorizontalLimits.x, _settings.HorizontalLimits.y);
            position.y = Mathf.Clamp(position.y, _settings.VerticalLimits.x, _settings.VerticalLimits.y);
            position.z = Mathf.Clamp(position.z, _settings.VerticalLimits.x, _settings.VerticalLimits.y);
            return position;
        }
        protected void Move(Vector3 nextPos)
        {
            _camTran.position = Vector3.Lerp(_camTran.position, nextPos, .8f);
        }

    }

}

