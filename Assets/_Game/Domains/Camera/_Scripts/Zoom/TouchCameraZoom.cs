using Zenject;

namespace Game.Camera
{
    using UnityEngine;
    public class TouchCameraZoom : CameraZoomBase
    {
        public TouchCameraZoom(Camera camera, ZoomSettings zoomSettings) : base(camera, zoomSettings)
        {
        }

        public override void Tick()
        {
            if (Input.touchCount == 2)
            {
                var touchZero = Input.GetTouch(0);
                var touchOne = Input.GetTouch(1);

                var touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                var touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                var prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                var currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                var diff = currentMagnitude - prevMagnitude;

                var increment = diff * 0.01f;

                Zoom(increment);
            }
        }


    }
}