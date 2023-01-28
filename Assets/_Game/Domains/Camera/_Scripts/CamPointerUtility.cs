using Udar;

namespace Game.Camera
{
    using UnityEngine;
    public class CamPointerUtility : ICameraPointerUtility
    {
        private readonly IUserInput _input;
        private readonly Camera _camera;

        private readonly Plane _planeZ;
        public CamPointerUtility(IUserInput userInput, Camera camera)
        {
            _input = userInput;
            _camera = camera;
            _planeZ = new Plane(Vector3.up, Vector3.zero);
        }

        public bool InputRaycast(out Vector3 point)
        {
            var ray = _camera.ScreenPointToRay(_input.GetPointerPosition());

            if (_planeZ.Raycast(ray, out float enter))
            {
                point = ray.GetPoint(enter);
                return true;
            }

            point = Vector3.zero;
            return false;
        }
        public Collider InputRaycast()
        {
            var ray = _camera.ScreenPointToRay(_input.GetPointerPosition());

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                return hit.collider;
            }

            return null;
        }

        public Vector3 CameraRaycast()
        {
            var ray = new Ray(_camera.transform.position, _camera.transform.forward);

            if (_planeZ.Raycast(ray, out float enter))
            {
                var point = ray.GetPoint(enter);
                return point;
            }

            return Vector3.zero;
        }

    }
}