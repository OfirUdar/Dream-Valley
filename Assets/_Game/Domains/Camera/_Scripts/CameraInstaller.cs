using Zenject;

namespace Game.Camera
{
    using UnityEngine;
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CameraSettingsSO _pcSettings;
        [SerializeField] private CameraSettingsSO _mobileSettings;

        public override void InstallBindings()
        {
            if (Application.isMobilePlatform)
                InstallMobile();
            else
                InstallPC();

            Container.Bind(typeof(ICameraController), typeof(ITickable))
                .To<CameraController>().AsSingle().NonLazy();

        }

        private void InstallMobile()
        {
            Container.Bind<CameraMoveBase>().To<TouchCameraMove>()
                   .AsSingle()
                  .WithArguments(_camera.transform)
                  .NonLazy();

            Container.Bind<CameraZoomBase>().To<TouchCameraZoom>()
               .AsSingle()
               .WithArguments(_camera)
               .NonLazy();

            Container.Bind<MoveSettings>().FromInstance(_mobileSettings.MoveSettings).AsSingle();
            Container.Bind<ZoomSettings>().FromInstance(_mobileSettings.ZoomSettings).AsSingle();
        }
        private void InstallPC()
        {
            Container.Bind<CameraMoveBase>().To<MouseCameraMove>()
                     .AsSingle()
                    .WithArguments(_camera.transform)
                    .NonLazy();

            Container.Bind<CameraZoomBase>().To<MouseCameraZoom>()
               .AsSingle()
               .WithArguments(_camera)
               .NonLazy();

            Container.Bind<MoveSettings>().FromInstance(_pcSettings.MoveSettings).AsSingle();
            Container.Bind<ZoomSettings>().FromInstance(_pcSettings.ZoomSettings).AsSingle();
        }
    }



}

