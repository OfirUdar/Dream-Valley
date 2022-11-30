namespace Game.Map.Element.Building
{
    public class UpgradeBuilder : IBuildingState
    {
        private readonly IDateTimer _timer;


        public UpgradeBuilder(IDateTimer timer)
        {
            _timer = timer;
        }

        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }

        public void Tick()
        {
            throw new System.NotImplementedException();
        }
    }
}