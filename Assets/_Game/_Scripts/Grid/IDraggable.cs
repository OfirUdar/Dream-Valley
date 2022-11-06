﻿namespace Game
{
    public interface IDraggable
    {
        public void StartDrag();
        public void OnDrag(bool canPlace);
        public void EndDrag();
    }
}