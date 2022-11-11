using Udar;
using UnityEngine;

namespace Game
{
    public class CamPointerUtility 
    {
        private readonly IUserInput _input;

        private readonly Plane _planeZ;
        public CamPointerUtility(IUserInput userInput)
        {
            _input = userInput;
            _planeZ = new Plane(Vector3.up, Vector3.zero);
        }

        public bool RaycastPointer(out Vector3 point)
        {
            var ray = CameraUtils.Main.ScreenPointToRay(_input.GetPointerPosition());

            if(_planeZ.Raycast(ray, out float enter))
            {
                point = ray.GetPoint(enter);
                return true;
            }

            point = Vector3.zero;
            return false;
        }
        public Collider RaycastPointer()
        {
            var ray = CameraUtils.Main.ScreenPointToRay(_input.GetPointerPosition());

            if (Physics.Raycast(ray,out RaycastHit hit, Mathf.Infinity))
            {
                return hit.collider;
            }

            return null;
        }
    }
}