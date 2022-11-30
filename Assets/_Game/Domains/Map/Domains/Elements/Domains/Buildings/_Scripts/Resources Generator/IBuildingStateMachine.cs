namespace Game.Map.Element.Building
{
    public interface IBuildingStateMachine
    {
        public void ChangeState(IBuildingState nextState);

    }
}