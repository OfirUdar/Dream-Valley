using System;
using UnityEngine;
using Zenject;

namespace Game.Map
{
    public class SelectionManager : ISelectionManager, ITickable
    {
        private readonly IUserInput _input;
        private readonly CamPointerUtility _camPointerUtility;

        private ISelectable _currentSelected;
        private bool _isLocked;


        private const float SHORT_PRESS = 0.1f;
        private float _pressingTimer;

        public event Action<ISelectable> SelectionChanged;


        public SelectionManager(IUserInput input, CamPointerUtility camPointerUtility)
        {
            _input = input;
            _camPointerUtility = camPointerUtility;
        }

        public void RequestSelect(ISelectable selectable)
        {
            if (_isLocked)
                return;

            if (_currentSelected == selectable)
                return;

            bool isSuccessSelected = selectable.Select();
            if (isSuccessSelected)
            {
                _currentSelected?.Unselect();
                _currentSelected = selectable;
                SelectionChanged?.Invoke(_currentSelected);
            }

        }
       
        public void RequestUnselect()
        {
            if (_isLocked)
                return;

            _currentSelected?.Unselect();
            _currentSelected = null;

            SelectionChanged?.Invoke(null);
        }
        public void Lock(bool isLock)
        {
            _isLocked = isLock;
        }

        public void Tick()
        {
            if (_isLocked)
                return;

            if (_input.IsPointerDown())
            {
                _pressingTimer = 0;
            }
            if (_input.IsPointerPressing())
            {
                _pressingTimer += Time.deltaTime;
            }
            if (_input.IsPointerUp() && !_input.IsPointerOverUI() && _pressingTimer <= SHORT_PRESS)
            {
                if (_camPointerUtility.InputRaycast() == null)
                    RequestUnselect();
            }
        }

    }

}