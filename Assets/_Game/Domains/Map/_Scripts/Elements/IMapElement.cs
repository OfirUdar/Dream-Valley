using System;

namespace Game.Map
{
    public interface IMapElement : IPlaceable, ISelectable, IDraggable, IDestroyable
    {
        public IPlaceApprover PlaceApprover { get; }
        public IInfoDisplayer InfoDisplayer { get; }
        public IUpgradeDisplayer UpgradeDisplayer { get; }
        public MapElementSO Data { get; }
        public MapElementSaveData SaveData { get; }


    }
    public interface IPlaceApprover
    {
        public void SubscribeForCallbacks(Action approveCallback, Action cancelCallback);
        public void Show();
        public void Hide();
    }
    public interface IDestroyable
    {
        public void Destroy();
    }

}