namespace Game.Map.Element
{
    public interface ILevelManager
    {
        public int CurrentLevel { get; }
        public void UpgradgeLevelUp();
    }
}
