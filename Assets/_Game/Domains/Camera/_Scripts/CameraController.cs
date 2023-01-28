using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Game.Camera
{
    public class CameraController : ICameraController, ITickable
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
            _moveHandler.SetActive(isCanMove);
            _canMove = isCanMove;
        }
        public void SetCanZoom(bool isCanZoom)
        {
            _canZoom = isCanZoom;
        }



        public async Task FocusAsync(Vector3 position, float zoom)
        {
            //Saves the last permissions and activate it at the end
            bool canMove = _canMove;
            bool canZoom = _canZoom;

            SetActive(false);

            if (_moveHandler.IsCurrentPositionFar(position))
                await _zoomHandler.FocusAsync(20f, 0.2f, Ease.OutSine);

            var tasks = new Task[2];
            tasks[0] = _moveHandler.FocusAsync(position);
            tasks[1] = _zoomHandler.FocusAsync(zoom);

            await Task.WhenAll(tasks);

            SetCanMove(canMove);
            SetCanZoom(canZoom);

        }
    }
}
