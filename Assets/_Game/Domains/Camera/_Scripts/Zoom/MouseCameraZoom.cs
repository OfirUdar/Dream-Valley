using Zenject;

namespace Game.Camera
{
    using UnityEngine;
    public class MouseCameraZoom : CameraZoomBase
    {
        private readonly IUserInput _input;

        public MouseCameraZoom(Camera camera, ZoomSettings zoomSettings, IUserInput input) : base(camera, zoomSettings)
        {
            _input = input;
        }

        public override void Tick()
        {
            var scroll = _input.GetScroll();

            if (scroll != Vector2.zero)
            {
                Zoom(Mathf.Sign(scroll.y));
            }
        }


    }



}

