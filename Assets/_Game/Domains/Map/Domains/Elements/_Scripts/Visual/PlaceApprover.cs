using System;
using UnityEngine;

namespace Game.Map.Element
{
    public class PlaceApprover : MonoBehaviour, IPlaceApprover
    {
        private event Action _approveCallback;
        private event Action _cancelCallback;


        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
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
    }
    
}