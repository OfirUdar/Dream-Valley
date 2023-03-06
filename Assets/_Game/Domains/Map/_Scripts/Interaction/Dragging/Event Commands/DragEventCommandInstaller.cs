using UnityEngine;
using Zenject;

namespace Game.Map
{
    public class DragEventCommandInstaller : MonoInstaller
    {
        [SerializeField] private VFXData _vfxInfo;

        [InjectOptional(Id = GameEvent.ElementDragging)] private readonly AudioClipInfoSO _dragAudio;
        [InjectOptional(Id = GameEvent.ElementPlaced)] private readonly AudioClipInfoSO _placedAudio;
        [InjectOptional(Id = GameEvent.ElementPlacedError)] private readonly AudioClipInfoSO _placeErrorAudio;

        [Inject] private readonly ISoundsManager _sfxManager;
        [Inject] private readonly IVFXFactory _vfxFactory;

        public override void InstallBindings()
        {
            InstallPlaced();
            InstallPlacedError();
            InstallDragging();
        }
        private void InstallPlacedError()
        {

            var placedErrorSFXCommand = new PlaySFXCommand(_sfxManager, _placeErrorAudio);
           // var dragEventCommand = new CompositeEventCommand(placedErrorSFXCommand);

            Container.Bind<IEventCommand>()
               .WithId(GameEvent.ElementPlacedError)
               .FromInstance(placedErrorSFXCommand)
               .AsTransient();
        }

        private void InstallPlaced()
        {
            var placedVFXCommand = new PlayVFXCommand(_vfxFactory, _vfxInfo);
            var placedSFXCommand = new PlaySFXCommand(_sfxManager, _placedAudio);

            var dragEventCommand = new CompositeEventCommand(placedVFXCommand, placedSFXCommand);

            Container.Bind<IEventCommand>()
              .WithId(GameEvent.ElementPlaced)
              .FromInstance(dragEventCommand)
              .AsTransient();

        }
        private void InstallDragging()
        {
            var dragSFXCommand = new PlaySFXCommand(_sfxManager, _dragAudio);
            var dragSFXRateCommand = new RateExecuteDecoratorCommand(dragSFXCommand);

            //var dragEventCommand = new CompositeEventCommand(dragSFXRateCommand);

            Container.Bind<IEventCommand>()
              .WithId(GameEvent.ElementDragging)
              .FromInstance(dragSFXRateCommand)
              .AsTransient();

        }
    }
}