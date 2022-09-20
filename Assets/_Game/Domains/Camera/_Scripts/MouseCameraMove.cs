using UnityEngine;
using Zenject;

namespace Game.Camera
{
    public class MouseCameraMove : CameraMoveBase, ITickable
    {
       

        private Vector3 _dragOrigin;

        public MouseCameraMove(Transform camTran, IUserInput input, MoveSettings moveSettings) : base(camTran, input, moveSettings)
        {
        }

        public void Tick()
        {
            if (_input.IsPointerDown())
            {
                _dragOrigin = GetWorldPointerPosition();
            }

            if (_input.IsPointerPressing())
            {
                var currentPosition = GetWorldPointerPosition();

                var delta = _dragOrigin - currentPosition;

                var nextPos = _camTran.position + delta;
                nextPos = ConvertToValidPosition(nextPos);

                Move(nextPos);

                _dragOrigin = currentPosition;
            }


        }
       
    }

}

