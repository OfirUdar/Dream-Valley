namespace Game.Camera
{
    using UnityEngine;
    public abstract class CameraZoomBase
    {
        private readonly Camera _cam;
        private readonly ZoomSettings _settings;

        public abstract void Tick();

        public CameraZoomBase(Camera camera, ZoomSettings zoomSettings)
        {
            _cam = camera;
            _settings = zoomSettings;
        }


        protected void Zoom(float increment)
        {
            var nextZoom = _cam.orthographicSize - increment * _settings.Speed * Time.deltaTime;
            _cam.orthographicSize = Mathf.Clamp
                (nextZoom, _settings.Min, _settings.Max);
        }
    }
}