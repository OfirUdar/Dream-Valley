using UnityEngine;
using Zenject;

namespace Game
{
    public class TouchCameraZoom : ITickable
    {
        private readonly Camera _cam;
        private readonly IUserInput _input;
        private readonly ZoomSettings _settings;

        public TouchCameraZoom(Camera camera, IUserInput input, ZoomSettings zoomSettings)
        {
            _cam = camera;
            _input = input;
            _settings = zoomSettings;
        }
        public void Tick()
        {
            if(Input.touchCount==2)
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

        private void Zoom(float increment)
        {
            var nextZoom = _cam.orthographicSize - increment * _settings.Speed * Time.deltaTime;
            _cam.orthographicSize = Mathf.Clamp
                (nextZoom, _settings.Min, _settings.Max);
        }
    }
}