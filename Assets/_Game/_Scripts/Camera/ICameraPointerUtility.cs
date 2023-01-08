using UnityEngine;

namespace Game
{
    public interface ICameraPointerUtility
    {
        public bool InputRaycast(out Vector3 point);
        public Collider InputRaycast();
        public Vector3 CameraRaycast();

    }
}