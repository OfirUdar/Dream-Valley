namespace Game.Map.Element
{
    public interface ILevelManager
    {
        public int CurrentIndexLevel { get; }
        public Level CurrentLevel { get; }
        public Level NextLevel { get; }

        public Level GetLevel(int index);
        public void UpgradgeLevelUp();
    }
}
