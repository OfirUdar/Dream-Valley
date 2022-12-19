namespace Game.Map
{
    public interface IInfoDisplayer
    {
        public void Display();
    }
    public interface IUpgradeDisplayer : IInfoDisplayer
    {
    }

}