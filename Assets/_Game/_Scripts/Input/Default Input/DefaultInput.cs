using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class DefaultInput : IUserInput
    {
        public Vector3 GetPointerPosition()
        {
            return Input.mousePosition;
        }

        public Vector2 GetScroll()
        {
            return Input.mouseScrollDelta;
        }

        public bool IsPointerDown()
        {
            return Input.GetMouseButtonDown(0);
        }
        public bool IsPointerUp()
        {
            return Input.GetMouseButtonUp(0);
        }

        public bool IsPointerPressing()
        {
            return Input.GetMouseButton(0);
        }

        public bool IsPointerOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}


