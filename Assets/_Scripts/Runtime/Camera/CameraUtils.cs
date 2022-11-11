using UnityEngine;

namespace Udar
{
    public static class CameraUtils
    {
        private static Camera _mainCamera;
        public static Camera Main
        {
            get
            {
                if (_mainCamera == null)
                    _mainCamera = Camera.main;

                return _mainCamera;
            }
        }

    }
}