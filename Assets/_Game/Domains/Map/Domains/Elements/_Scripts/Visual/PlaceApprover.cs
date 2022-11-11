using System;
using UnityEngine;

namespace Game.Map.Element
{
    public class PlaceApprover : MonoBehaviour, IPlaceApprover
    {
        private Action _approveCallback;
        private Action _cancelCallback;


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
        }
        public void Cancel()
        {
            _cancelCallback?.Invoke();
        }

        public void SubscribeForCallbacks(Action approveCallback, Action cancelCallback)
        {
            _approveCallback = approveCallback;
            _cancelCallback = cancelCallback;
        }
    }
    public interface IPlaceApprover
    {
        public void SubscribeForCallbacks(Action approveCallback, Action cancelCallback);
        public void Show();
        public void Hide();
    }
}