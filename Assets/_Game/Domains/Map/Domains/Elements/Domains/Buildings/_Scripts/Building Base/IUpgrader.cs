using System;

namespace Game.Map.Element.Building
{
    public interface IUpgrader
    {
        public event Action UpgradeStarted;
        public event Action UpgradeFinished;
    }
}