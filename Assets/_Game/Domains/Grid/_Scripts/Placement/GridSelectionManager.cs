using System;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GridSelectionManager : ISelectionManager, ITickable
    {
        private readonly IUserInput _input;
        private readonly CamPointerUtility _camPointer;
        private ISelectable _currentSelected;

        private float _timer = 0;

        public event Action<ISelectable> OnSelectionChanged;

        public GridSelectionManager(IUserInput input, CamPointerUtility camPointer)
        {
            _input = input;
            _camPointer = camPointer;
        }

        public void Tick()
        {
            CheckForUnselection();
            _timer += Time.deltaTime;
        }

        private void CheckForUnselection()
        {
            if (_input.IsPointerDown())
            {
                if (_input.IsPointerOverUI())
                {
                    if (_currentSelected != null)
                        Unselect(_currentSelected);
                    return;
                }
                _timer = 0;
            }
            if (_input.IsPointerUp() && _currentSelected != null && _timer < 0.2f)
            {
                var collider = _camPointer.RaycastPointer();
                if (collider == null)
                {
                    Unselect(_currentSelected);
                    return;
                }
                if (collider.TryGetComponent(out ISelectable selectable))
                {
                    if (selectable != _currentSelected)
                        Unselect(_currentSelected);
                }
            }
        }

        public bool TrySelect(ISelectable selectable)
        {
            _currentSelected?.Unselect();
            _currentSelected = selectable;
            _currentSelected.Select();

            OnSelectionChanged?.Invoke(selectable);

            return true;
        }

        public void Unselect(ISelectable selectable)
        {
            selectable.Unselect();
            _currentSelected = null;

            OnSelectionChanged?.Invoke(null);

        }
    }
    public interface ISelectionManager
    {
        public event Action<ISelectable> OnSelectionChanged;
        public bool TrySelect(ISelectable selectable);
        public void Unselect(ISelectable selectable);
    }
}