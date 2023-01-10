using System;
using Udar;
using UnityEngine;

namespace Game.Map.Element
{
    public class PlaceApprover : MonoBehaviour, IPlaceApprover
    {
        [SerializeField] private CanvasActivator _canvasActivator;
        [Space]
        [SerializeField] private UdarCanvasGroup _acceptButton;
        private event Action _approveCallback;
        private event Action _cancelCallback;

        public void Hide()
        {
            _canvasActivator.Deactivate();
        }

        public void Show()
        {
            _canvasActivator.Activate();
        }

        public void Approve()
        {
            _approveCallback?.Invoke();
            _approveCallback = null;
        }
        public void Cancel()
        {
            _cancelCallback?.Invoke();
            _cancelCallback = null;
        }

        public void SubscribeForCallbacks(Action approveCallback, Action cancelCallback)
        {
            _approveCallback += approveCallback;
            _cancelCallback += cancelCallback;
        }

        public void SetPlaceAvailbility(bool isAvailable)
        {
            _acceptButton.Activate(isAvailable);
        }
    }

}