using System;

namespace Game.Map
{
    public interface IMapElement : IPlaceable, ISelectable, IDraggable, IDestroyable
    {
        public IPlaceApprover PlaceApprover { get; }
        public IOptionsDisplayer OptionsDisplayer { get; }
        public IInfoDisplayer InfoDisplayer { get; }
        public IUpgradeDisplayer UpgradeDisplayer { get; }
        public IRemoveHandler RemoveHandler { get; }

        public IEventor Eventor { get; }
        public MapElementSO Data { get; }
        public MapElementSaveData SaveData { get; }


    }
    public interface IPlaceApprover
    {
        public void SubscribeForCallbacks(Action approveCallback, Action cancelCallback);
        public void SetPlaceAvailbility(bool isAvailable);
        public void Show();
        public void Hide();
    }
    public interface IDestroyable
    {
        public void Destroy();
    }

}