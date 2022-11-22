using Udar;
using Zenject;

namespace Game
{
    public class SaveLoadInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(
                typeof(ISaveManager)
                ,typeof(ILoadManager))
                .To<SaveLoadManager>().AsSingle();
        }
    }
}
