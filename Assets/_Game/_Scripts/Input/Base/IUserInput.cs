
using UnityEngine;

namespace Game
{
    public interface IUserInput
    {
        public bool IsPointerDown();
        public bool IsPointerUp();
        public bool IsPointerPressing();
        public bool IsPointerOverUI();
        public Vector3 GetPointerPosition();
        public Vector2 GetScroll();
    }
}

