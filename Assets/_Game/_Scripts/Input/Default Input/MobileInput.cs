using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class MobileInput : IUserInput
    {
        public Vector3 GetPointerPosition()
        {
            return Input.GetTouch(0).position;
        }

        public Vector2 GetScroll()
        {
            return Input.GetTouch(0).deltaPosition;
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
            for (int i = 0; i < Input.touches.Length; i++)
            {
                if (EventSystem.current.IsPointerOverGameObject(i))
                    return true;
            }
            return false;
           
        }
    }
}


