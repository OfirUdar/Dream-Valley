namespace Game.Map.Element.Building
{
    public interface IBuildingState
    {
        public void Enter();
        public void Tick();
        public void Exit();
    }
}