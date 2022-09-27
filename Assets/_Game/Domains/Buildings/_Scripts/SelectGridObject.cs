using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Game
{
    public class SelectGridObject : MonoBehaviour, ISelectable
    {
        private ISelectionManager _selectionManager;

        public bool IsSelected { get; private set; }

        public UnityEvent<bool> OnSelectionChanged;

        [Inject]
        public void Init(ISelectionManager selectionManager)
        {
            _selectionManager = selectionManager;
        }


        private void OnMouseUpAsButton()
        {
            Debug.Log("OnMouseUpAsButton");
            TriggerSelection();
        }
        public void TriggerSelection()
        {
            if (IsSelected)
                _selectionManager.Unselect(this);
            else
                _selectionManager.TrySelect(this);
        }
        public void Select()
        {
            IsSelected = true;
            OnSelectionChanged?.Invoke(IsSelected);
        }
        public void Unselect()
        {
            IsSelected = false;
            OnSelectionChanged?.Invoke(IsSelected);
        }
    }


}
