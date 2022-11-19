namespace Game.Camera
{
    using DG.Tweening;
    using System.Collections;
    using System.Threading.Tasks;
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

      
        public async Task FocusAsync(float nextZoom, float duration = 0.5f, Ease ease = Ease.InOutSine)
        {
            await _cam.DOOrthoSize(nextZoom, duration).SetEase(ease).Play().AsyncWaitForCompletion();
        }
    }
}