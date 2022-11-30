using Zenject;

namespace Game
{
    public class TimerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(ITickable),typeof(IDateTimer)).To<Timer>().AsSingle().NonLazy();
        }
    }

}