using System;

namespace Game.Map
{
    public interface IMapElement : IPlaceable, ISelectable, IDraggable
    {
       // public ElementOption ElementOptions { get; }
        public IPlaceApprover PlaceApprover { get; }
        public void Destroy();
    }
    public interface IPlaceApprover
    {
        public void SubscribeForCallbacks(Action approveCallback, Action cancelCallback);
        public void Show();
        public void Hide();
    }

}