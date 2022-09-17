using UnityEngine;

namespace Game
{
    public class MobileInput : IUserInput
    {
        public Vector3 GetPointerPosition()
        {
            return Input.mousePosition;
        }

        public Vector2 GetScroll()
        {
            return Input.GetTouch(0).deltaPosition;
        }

        public bool IsPointerDown()
        {
            return Input.GetMouseButtonDown(0);
        }
        public bool IsPointerPressing()
        {
            return Input.GetMouseButton(0);
        }
    }
}


