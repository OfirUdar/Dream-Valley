using UnityEngine;
using Zenject;

namespace Game.Map
{
    public class InteractionManagerInstaller : MonoInstaller
    {
        [SerializeField] private DraggingSounds _draggingSounds;
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
            Container.BindInterfacesAndSelfTo<ExistDragState>().AsSingle().WithArguments(_draggingSounds);
            Container.BindInterfacesAndSelfTo<NewDragState>().AsSingle().WithArguments(_draggingSounds);

            Container.Bind<IDragManager>().To<DragManager>().AsSingle();
        }
    }

}
