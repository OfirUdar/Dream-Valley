using System;
using Udar;
using UnityEngine;

namespace Game.Map.Element
{
    public class PointerEvents : MonoBehaviour
    {
        public event Action UpAsButton;
        public event Action StartDrag;
        public event Action EndDrag;
        public event Action Dragging;

        private bool _isDragging;
        private Vector3 _startDragPosition;

        private void OnMouseUpAsButton()
        {
            UpAsButton?.Invoke();
        }

        private void OnMouseDown()
        {
            _startDragPosition = CameraUtils.Main.ScreenToWorldPoint(Input.mousePosition);
        }
        private void OnMouseUp()
        {
            if (_isDragging)
            {
                _isDragging = false;
                EndDrag?.Invoke();
            }
        }
        private void OnMouseDrag()
        {
            if (!_isDragging)
            {
                var currentPosition = CameraUtils.Main.ScreenToWorldPoint(Input.mousePosition);
                if ((currentPosition - _startDragPosition).sqrMagnitude > 0.05f)
                {
                    _isDragging = true;
                    StartDrag?.Invoke();
                }
                else
                    return;
            }

            Dragging?.Invoke();

        }
    }
}

