using DG.Tweening;
using System.Threading.Tasks;
namespace Game.Camera
{
    using UnityEngine;
    public abstract class CameraMoveBase
    {
        protected readonly Camera _camera;
        protected readonly Transform _cameraTransform;
        protected readonly IUserInput _input;
        protected readonly MoveSettings _settings;

        public abstract void Tick();
        public abstract void SetActive(bool isActive);

        public CameraMoveBase(Camera camera, IUserInput input, MoveSettings moveSettings)
        {
            _camera = camera;
            _cameraTransform = _camera.transform;
            _input = input;
            _settings = moveSettings;
        }

        protected Vector3 GetWorldPointerPosition()
        {
            var pointerPos = _input.GetPointerPosition();
            var position = _camera.ScreenToWorldPoint(pointerPos);
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
            _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, nextPos, _settings.LerpAmount * Time.deltaTime);
        }

        public bool IsCurrentPositionFar(Vector3 targetPosition)
        {
            var distance = 7f;
            return (_cameraTransform.position - targetPosition).sqrMagnitude >= distance;
        }
        public async Task FocusAsync(Vector3 nextPosition, float duration = 0.5f, Ease ease = Ease.InOutSine)
        {
            await _cameraTransform.DOMove(nextPosition, duration).SetEase(ease).Play().AsyncWaitForCompletion();
        }

    }

}

