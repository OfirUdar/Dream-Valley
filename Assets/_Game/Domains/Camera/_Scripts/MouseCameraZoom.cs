using UnityEngine;
using Zenject;

namespace Game
{
    public class MouseCameraZoom : ITickable
    {
        private readonly Camera _cam;
        private readonly IUserInput _input;
        private readonly ZoomSettings _settings;

        public MouseCameraZoom(Camera camera, IUserInput input, ZoomSettings zoomSettings)
        {
            _cam = camera;
            _input = input;
            _settings = zoomSettings;
        }
        public void Tick()
        {
            var scroll = _input.GetScroll();

            if (scroll != Vector2.zero)
            {
                Zoom(Mathf.Sign(scroll.y));
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

