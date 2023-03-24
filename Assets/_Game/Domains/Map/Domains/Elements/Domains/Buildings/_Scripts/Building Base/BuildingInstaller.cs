using UnityEngine;
using Zenject;

namespace Game.Map.Element.Building
{
    public class BuildingInstaller : MonoInstaller
    {
        [Header("Upgrade Completed:")]
        [SerializeField] private AudioClipInfoSO _upgradeCompletedAudioClip;
        [SerializeField] private GameObject _upgradeCompletedVFXPfb;
        [Header("Upgrade Started:")]
        [SerializeField] private AudioClipInfoSO _upgradeStartedAudioClip;
        [SerializeField] private GameObject _upgradeStartedVFXPfb;

        public override void InstallBindings()
        {
            Container.Bind(typeof(ITickable), typeof(IDateTimer)).To<Timer>().AsSingle();

            Container.BindInterfacesTo<BuildingStateMachine>().AsSingle();

            //Install Upgrade
            Container.BindInterfacesAndSelfTo<UpgradeState>().AsSingle();

            Container.Bind<IBuildingState>()
               .WithId(StateType.Upgrade)
               .To<UpgradeState>().FromResolve();

            InstallEventCommands();

        }

        private void InstallEventCommands()
        {
            InstallUpgradeCompletedEventCommand();
            InstallUpgradeStartedEventCommand();
        }

        private void InstallUpgradeCompletedEventCommand()
        {
            Container.Bind<IEventCommand>().To<PlaySFXCommand>().AsTransient()
               .WithArguments(_upgradeCompletedAudioClip).WhenInjectedInto<UpgradeCompletedCommand>();

            Container.Bind<IEventCommand>().To<PlayVFXCommand>().AsTransient()
                .WithArguments(_upgradeCompletedVFXPfb).WhenInjectedInto<UpgradeCompletedCommand>();


            Container.BindMemoryPool<IEventCommand, UpgradeCompletedCommand.Pool>()
                .To<UpgradeCompletedCommand>();
        }
        private void InstallUpgradeStartedEventCommand()
        {
            Container.Bind<IEventCommand>().To<PlaySFXCommand>().AsTransient()
               .WithArguments(_upgradeStartedAudioClip).WhenInjectedInto<UpgradeStartedCommand>();

            Container.Bind<IEventCommand>().To<PlayVFXCommand>().AsTransient()
                .WithArguments(_upgradeStartedVFXPfb).WhenInjectedInto<UpgradeStartedCommand>();


            Container.BindMemoryPool<IEventCommand, UpgradeStartedCommand.Pool>()
                .To<UpgradeStartedCommand>();
        }
    }

}