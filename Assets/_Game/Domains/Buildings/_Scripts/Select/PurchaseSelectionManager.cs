using System;
using System.Collections.Generic;
using Zenject;

namespace Game
{
    public class PurchaseSelectionManager : ISelectionManager, IInitializable,
        ILateDisposable
    {
        private readonly IUserInput _input;
        private readonly CamPointerUtility _camPointer;
        private readonly SelectionEventAggragator _selectionEventAggragator;


        public List<ISelectable> Selection { get; } = new List<ISelectable>();

        public event Action<ISelectable> SelectionChanged;

        public PurchaseSelectionManager(IUserInput input, CamPointerUtility camPointer, SelectionEventAggragator selectionEventAggragator)
        {
            _input = input;
            _camPointer = camPointer;
            _selectionEventAggragator = selectionEventAggragator;
        }

        public void Initialize()
        {
            _selectionEventAggragator.SelectRequested += OnSelectRequested;
        }
        public void LateDispose()
        {
            _selectionEventAggragator.SelectRequested -= OnSelectRequested;
            foreach (var selectable in Selection)
            {
                selectable.Unselect();
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
                return false;
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


   
}