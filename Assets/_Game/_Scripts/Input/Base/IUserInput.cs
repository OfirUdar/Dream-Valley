
using UnityEngine;

namespace Game
{
    public interface IUserInput
    {
        public bool IsPointerDown();
        public bool IsPointerPressing();
        public Vector3 GetPointerPosition();
        public Vector2 GetScroll();
    }
}

