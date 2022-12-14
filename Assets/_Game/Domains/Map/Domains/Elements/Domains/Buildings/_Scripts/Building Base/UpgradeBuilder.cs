namespace Game.Map.Element.Building
{
    public class UpgradeBuilder : IBuildingState
    {
        private readonly IBuildingStateMachine _machine;
        private readonly IDateTimer _timer;


        public UpgradeBuilder(IBuildingStateMachine machine, IDateTimer timer)
        {
            _machine = machine;
            _timer = timer;
        }

        public void Enter()
        {
            _timer.Finished += OnTimerFinished;
            //_timer.SetTime().Start();
        }

        public void Exit()
        {
            _timer.Finished -= OnTimerFinished;

            _timer.Stop();
        }

        public void Tick()
        {

        }

        private void OnTimerFinished()
        {
            _machine.ChangeState(StateType.Active);
        }

    }
}