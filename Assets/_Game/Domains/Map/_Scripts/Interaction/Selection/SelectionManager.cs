using System;
using UnityEngine;
using Zenject;

namespace Game.Map
{
    public class SelectionManager : ITickable
    {
        private readonly IUserInput _input;
        private readonly CamPointerUtility _camPointerUtility;

        private ISelectable _currentSelected;


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
            if (_currentSelected == selectable)
                return;

            _currentSelected?.Unselect();
            _currentSelected = selectable;
            _currentSelected.Select();

            SelectionChanged?.Invoke(_currentSelected);
        }
        public void RequestUnselect()
        {
            _currentSelected?.Unselect();
            _currentSelected = null;

            SelectionChanged?.Invoke(null);
        }


        public void Tick()
        {

            if (_input.IsPointerDown())
            {
                _pressingTimer = 0;
            }
            if (_input.IsPointerPressing())
            {
                _pressingTimer += Time.deltaTime;
            }
            if (_input.IsPointerUp() && _pressingTimer <= SHORT_PRESS)
            {
                if (_camPointerUtility.RaycastPointer() == null)
                    RequestUnselect();
            }
        }

    }

}