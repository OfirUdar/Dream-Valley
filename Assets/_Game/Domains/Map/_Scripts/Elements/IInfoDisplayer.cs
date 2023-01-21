using UnityEngine;

namespace Game.Map
{
    public interface IInfoDisplayer
    {
        public void Display();
    }
    public interface IUpgradeDisplayer : IInfoDisplayer
    {

    }
    public interface IOptionsDisplayer
    {
        public void Show(Transform container);
        public string GetDisplayText();
    }

    public interface IRemoveHandler
    {
        public ResourcePrice Price { get; }
        public void Remove();
    }

}