namespace Game
{
    public interface IEventCommand
    {
        public void Execute(object value = null);
    }
}