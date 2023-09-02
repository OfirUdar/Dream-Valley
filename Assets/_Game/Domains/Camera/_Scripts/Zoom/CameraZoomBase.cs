namespace Game.Camera
{
    using DG.Tweening;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.Rendering.Universal;
    public abstract class CameraZoomBase
    {
        protected readonly Camera _cam;
        private readonly UniversalAdditionalCameraData _camData;
        private readonly ZoomSettings _settings;


        public abstract void Tick();

        public CameraZoomBase(Camera camera, ZoomSettings zoomSettings)
        {
            _cam = camera;
            _camData = _cam.GetUniversalAdditionalCameraData();
            _settings = zoomSettings;
        }


        protected void Zoom(float increment)
        {
            var nextZoom = _cam.orthographicSize - increment * _settings.Speed * Time.deltaTime;
            var finalZoom = Mathf.Clamp(nextZoom, _settings.Min, _settings.Max);

            _cam.orthographicSize = finalZoom;

            foreach (var camera in _camData.cameraStack)
            {
                camera.orthographicSize = finalZoom;
            }
        }


        public async Task FocusAsync(float nextZoom, float duration = 0.5f, Ease ease = Ease.InOutSine)
        {
            await _cam.DOOrthoSize(nextZoom, duration).SetEase(ease).Play().AsyncWaitForCompletion();
           
            foreach (var camera in _camData.cameraStack)
            {
                camera.orthographicSize = nextZoom;
            }
        }
    }
}