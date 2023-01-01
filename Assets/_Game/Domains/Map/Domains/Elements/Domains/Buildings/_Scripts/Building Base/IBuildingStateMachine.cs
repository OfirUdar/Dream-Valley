namespace Game.Map.Element.Building
{
    public interface IBuildingStateMachine
    {
        public StateType GetCurrentState();
        public void ChangeState(IBuildingState nextState);
        public void ChangeState(StateType state);

    }
    public enum StateType
    {
        Active,
        Upgrade,
    }
}