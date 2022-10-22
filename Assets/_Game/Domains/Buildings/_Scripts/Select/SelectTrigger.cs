using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Game
{
    public class SelectTrigger : MonoBehaviour, ISelectable
    {
        private SelectionEventAggragator _selectionEventAggragator;
        private float _timer;
        public bool IsSelected { get; private set; }

        public UnityEvent<bool> OnSelectionChanged;

        [Inject]
        public void Init(SelectionEventAggragator selectionEventAggragator)
        {
            _selectionEventAggragator = selectionEventAggragator;
        }

        private void OnMouseDown()
        {
            _timer = 0;
        }
        private void OnMouseUpAsButton()
        {
            if (_timer > 0.1f)
                return;
            TriggerSelection();
        }

        private void Update()
        {
            _timer += Time.deltaTime;
        }

        public void TriggerSelection()
        {
            if (IsSelected)
                _selectionEventAggragator.Unselect(this);
            else
                _selectionEventAggragator.RequestSelect(this);
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
