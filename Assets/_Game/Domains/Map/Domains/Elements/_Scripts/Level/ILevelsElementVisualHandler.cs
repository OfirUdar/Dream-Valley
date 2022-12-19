namespace Game.Map.Element
{
    public interface ILevelsElementVisualHandler
    {
        /// <summary>
        /// Refersh element visualing - it can be after upgradeing element to next level
        /// </summary>
        public void Refresh(int level);
    }
}
