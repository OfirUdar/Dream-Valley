using Zenject;

namespace Game.Camera
{
    using UnityEngine;
    public class CameraControllerInstaller : MonoInstaller
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

            Container.Bind<Camera>().FromInstance(_camera);
        }

        private void InstallMobile()
        {
            Container.Bind<CameraMoveBase>().To<TouchCameraMove>()
                   .AsSingle()
                  .NonLazy();

            Container.Bind<CameraZoomBase>().To<TouchCameraZoom>()
               .AsSingle()
               .NonLazy();

            Container.Bind<MoveSettings>().FromInstance(_mobileSettings.MoveSettings).AsSingle();
            Container.Bind<ZoomSettings>().FromInstance(_mobileSettings.ZoomSettings).AsSingle();
        }
        private void InstallPC()
        {
            Container.Bind<CameraMoveBase>().To<MouseCameraMove>()
                     .AsSingle()
                    .NonLazy();

            Container.Bind<CameraZoomBase>().To<MouseCameraZoom>()
               .AsSingle()
               .NonLazy();

            Container.Bind<MoveSettings>().FromInstance(_pcSettings.MoveSettings).AsSingle();
            Container.Bind<ZoomSettings>().FromInstance(_pcSettings.ZoomSettings).AsSingle();
        }
    }



}

