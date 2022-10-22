using Zenject;

namespace Game
{
    public class IdleEditStateInstaller : MonoInstaller<IdleEditStateInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(ISelectionManager), typeof(IInitializable),
                typeof(ILateDisposable), typeof(ITickable))
                .To<GridSelectionManager>()
                .AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<DragController>()
                .AsSingle().NonLazy();
           
        }
    }

}
