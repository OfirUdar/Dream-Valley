﻿using System;

namespace Game.Map
{
    public interface IMapElement : IPlaceable, ISelectable, IDraggable
    {
        public IPlaceApprover PlaceApprover { get; }
        public MapElementSO Data { get; }
        public MapElementSaveData SaveData { get; }

        public void Destroy();

    }
    public interface IPlaceApprover
    {
        public void SubscribeForCallbacks(Action approveCallback, Action cancelCallback);
        public void Show();
        public void Hide();
    }

}