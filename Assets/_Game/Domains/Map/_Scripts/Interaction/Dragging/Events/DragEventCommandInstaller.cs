using UnityEngine;
using Zenject;

namespace Game.Map
{
    public class DragEventCommandInstaller : MonoInstaller
    {
        [SerializeField] private VFXData _vfxInfo;

        [SerializeField] private AudioClipInfoSO _dragAudio;
        [SerializeField] private AudioClipInfoSO _placedAudio;
        [SerializeField] private AudioClipInfoSO _placeErrorAudio;

        public override void InstallBindings()
        {
            InstallPlaced();
            InstallDragging();
            InstallPlacedError();
        }
        private void InstallPlacedError()
        {
            Container.Bind<IEventCommand>().To<PlaySFXCommand>().AsTransient()
               .WithArguments(_placeErrorAudio).WhenInjectedInto<DragEndPlacedErrorCommand>();


            Container.BindMemoryPool<IEventCommand, DragEndPlacedErrorCommand.Pool>()
                .To<DragEndPlacedErrorCommand>();
        }

        private void InstallPlaced()
        {
            Container.Bind<IEventCommand>().To<PlaySFXCommand>().AsTransient()
               .WithArguments(_placedAudio).WhenInjectedInto<CompositeEventCommand>();

            Container.Bind<IEventCommand>().To<PlayVFXCommand>().AsTransient()
                .WithArguments(_vfxInfo).WhenInjectedInto<CompositeEventCommand>();


            Container.Bind<IEventCommand>().To<CompositeEventCommand>().AsTransient()
                .WhenInjectedInto<DragEndPlacedCommand>();

            Container.BindMemoryPool<IEventCommand, DragEndPlacedCommand.Pool>()
                .To<DragEndPlacedCommand>();
        }
        private void InstallDragging()
        {
            Container.Bind<IEventCommand>().To<PlaySFXCommand>().AsTransient()
                .WithArguments(_dragAudio).WhenInjectedInto<RateExecuteDecoratorCommand>();

            Container.Bind<IEventCommand>()
                .To<RateExecuteDecoratorCommand>().AsTransient()
                .WhenInjectedInto<DraggingCommand>();


            Container.BindMemoryPool<IEventCommand, DraggingCommand.Pool>()
                .To<DraggingCommand>();

        }
    }


}