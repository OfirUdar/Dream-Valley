using Zenject;

namespace Game.Camera
{
    public class CameraController : ITickable
    {
        private readonly CameraMoveBase _moveHandler;
        private readonly CameraZoomBase _zoomHandler;


        private bool _canMove;
        private bool _canZoom;
        public CameraController(CameraMoveBase moveHandler, CameraZoomBase zoomHandler)
        {
            _moveHandler = moveHandler;
            _zoomHandler = zoomHandler;

            _canMove = true;
            _canZoom = true;
        }

        public void Tick()
        {
            if (_canMove)
                _moveHandler.Tick();
            if (_canZoom)
                _zoomHandler.Tick();
        }

        public void SetActive(bool isActive)
        {
            SetCanMove(isActive);
            SetCanZoom(isActive);
        }
        public void SetCanMove(bool isCanMove)
        {
            _canMove = isCanMove;
        }
        public void SetCanZoom(bool isCanZoom)
        {
            _canZoom = isCanZoom;
        }
    }
}