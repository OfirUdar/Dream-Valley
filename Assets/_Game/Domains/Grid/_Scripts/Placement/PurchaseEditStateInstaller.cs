using Zenject;

namespace Game
{
    public class PurchaseEditStateInstaller : MonoInstaller<PurchaseEditStateInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(ISelectionManager), typeof(IInitializable),
                typeof(ILateDisposable))
                .To<PurchaseSelectionManager>()
                .AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<PurchaseDragController>()
                .AsSingle().NonLazy();
           
        }
    }

}
