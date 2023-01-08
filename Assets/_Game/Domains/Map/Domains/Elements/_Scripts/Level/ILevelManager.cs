using System;

namespace Game.Map.Element
{
    public interface ILevelManager
    {
        public event Action LevelUp;
        public int CurrentIndexLevel { get; }
        public Level CurrentLevel { get; }
        public Level NextLevel { get; }

        public bool HasNext();
        public Level GetLevel(int index);
        public void UpgradeLevelUp();
    }
}
