﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GridSelectionManager : ISelectionManager, IInitializable,ILateDisposable,ITickable
    {
        private readonly IUserInput _input;
        private readonly CamPointerUtility _camPointer;
        private readonly SelectionEventAggragator _selectionEventAggragator;

        private float _timer = 0;

        public List<ISelectable> Selection { get; } = new List<ISelectable>();

        public event Action<ISelectable> SelectionChanged;

        public GridSelectionManager(IUserInput input, CamPointerUtility camPointer, SelectionEventAggragator selectionEventAggragator)
        {
            _input = input;
            _camPointer = camPointer;
            _selectionEventAggragator = selectionEventAggragator;
        }

        public void Initialize()
        {
            _selectionEventAggragator.SelectRequested += OnSelectRequested;
            _selectionEventAggragator.UnselectRequested += OnUnselectRequested;
        }
        public void LateDispose()
        {
            _selectionEventAggragator.SelectRequested -= OnSelectRequested;
            _selectionEventAggragator.UnselectRequested -= OnUnselectRequested;

            foreach (var selectable in Selection)
            {
                selectable.Unselect();
            }
        }

        
        public void Tick()
        {
            CheckForUnselection();
            _timer += Time.deltaTime;
        }

        private void CheckForUnselection()
        {
            if (_input.IsPointerDown() && !_input.IsPointerOverUI())
            {
                _timer = 0;
            }
            if (_input.IsPointerUp()
                && Selection.Count > 0
                && _timer < 0.1f)
            {
                var selection = Selection[0];

                var collider = _camPointer.RaycastPointer();
                if (collider == null)
                {
                    Unselect(selection);
                    return;
                }
                if (collider.TryGetComponent(out ISelectable selectable))
                {
                    if (selectable != selection)
                        Unselect(selection);
                }
            }
        }

        private void OnSelectRequested(ISelectable selectable)
        {
            TrySelect(selectable);
        }
        private void OnUnselectRequested(ISelectable selectable)
        {
            Unselect(selectable);
        }

        public bool TrySelect(ISelectable selectable)
        {
            if (Selection.Count > 0)
            {
                Selection[0].Unselect();
                Selection[0] = selectable;
            }
            else
            {
                Selection.Add(selectable);
            }

            Selection[0].Select();

            SelectionChanged?.Invoke(selectable);

            return true;
        }

        public void Unselect(ISelectable selectable)
        {
            selectable.Unselect();
            Selection.Remove(selectable);

            SelectionChanged?.Invoke(null);

        }


    }


    public interface ISelectionManager
    {
        public List<ISelectable> Selection { get; }
        public event Action<ISelectable> SelectionChanged;
        public bool TrySelect(ISelectable selectable);
        public void Unselect(ISelectable selectable);
    }
}