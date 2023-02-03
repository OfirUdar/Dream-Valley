namespace Game.Map
{
    public interface IEventCommand
    {
        public void Execute(object value = null);
    }
}