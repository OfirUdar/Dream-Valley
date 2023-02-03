using UnityEngine;
using Zenject;

namespace Game.Map
{
    public class DragEventCommandInstaller : MonoInstaller
    {
        [SerializeField] private AudioClipInfo _dragAudio;
        [Space]
        [SerializeField] private AudioClipInfo _placedAudio;
        [SerializeField] private VFXData _vfxInfo;
        [Space]
        [SerializeField] private AudioClipInfo _placeErrorAudio;

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
            var dragEventCommand = new DragEventCommand(placedErrorSFXCommand);

            Container.Bind<IEventCommand>()
               .WithId("Placed_Error")
               .FromInstance(dragEventCommand)
               .AsTransient();
        }

        private void InstallPlaced()
        {
            var placedVFXCommand = new PlayVFXCommand(_vfxFactory, _vfxInfo);
            var placedSFXCommand = new PlaySFXCommand(_sfxManager, _placedAudio);

            var dragEventCommand = new DragEventCommand(placedVFXCommand, placedSFXCommand);

            Container.Bind<IEventCommand>()
              .WithId("Placed")
              .FromInstance(dragEventCommand)
              .AsTransient();

        }
        private void InstallDragging()
        {
            var dragSFXCommand = new PlaySFXCommand(_sfxManager, _dragAudio);
            var dragSFXRateCommand = new PlaySFXWithRateDecoratorCommand(dragSFXCommand);

            var dragEventCommand = new DragEventCommand(dragSFXRateCommand);

            Container.Bind<IEventCommand>()
              .WithId("Dragging")
              .FromInstance(dragEventCommand)
              .AsTransient();

        }
    }
}