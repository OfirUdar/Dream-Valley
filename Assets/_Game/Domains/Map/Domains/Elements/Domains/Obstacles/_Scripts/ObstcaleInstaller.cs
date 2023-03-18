using UnityEngine;
using Zenject;

namespace Game.Map.Element.Obstcales
{
    public class ObstcaleInstaller : MonoInstaller
    {
        [SerializeField] private ObstacleDataSO _obstacleDataSO;
        [SerializeField] private AudioClipInfoSO _removeAudio;
        [SerializeField] private VFXData _vfxData;

        public override void InstallBindings()
        {
            Container.Bind<ObstacleDataSO>().FromInstance(_obstacleDataSO);

            Container.BindInterfacesTo<ObstacleRemoveHandler>().AsSingle();

            InstallEventCommands();
        }

        private void InstallEventCommands()
        {
            Container.Bind<IEventCommand>().To<PlaySFXCommand>().AsTransient()
                .WithArguments(_removeAudio).WhenInjectedInto<CompositeEventCommand>();

            Container.Bind<IEventCommand>().To<PlayVFXCommand>().AsTransient()
                .WithArguments(_vfxData).WhenInjectedInto<CompositeEventCommand>();


            Container.Bind<IEventCommand>().To<CompositeEventCommand>().AsTransient()
                .WhenInjectedInto<RemoveObstacleCommand>();

            Container.BindMemoryPool<IEventCommand, RemoveObstacleCommand.Pool>()
                .To<RemoveObstacleCommand>();
        }
    }
}
