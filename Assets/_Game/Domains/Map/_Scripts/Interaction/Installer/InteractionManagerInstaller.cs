using Zenject;

namespace Game.Map
{
    public class InteractionManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallSelection();
            InstallDragging();
        }
   
        private void InstallSelection()
        {
            Container.Bind(typeof(ITickable),typeof(ISelectionManager)).To<SelectionManager>()
              .AsSingle().NonLazy();
        }

        private void InstallDragging()
        {
            Container.BindInterfacesAndSelfTo<ExistDragState>().AsSingle();
            Container.BindInterfacesAndSelfTo<NewDragState>().AsSingle();

            Container.Bind<IDragManager>().To<DragManager>().AsSingle();

        }
    }

}
