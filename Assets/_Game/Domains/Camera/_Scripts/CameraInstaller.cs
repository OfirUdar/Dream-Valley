using Zenject;

namespace Game.Camera
{
    using UnityEngine;
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;

        public override void InstallBindings()
        {
            if (Application.isMobilePlatform)
                InstallMobile();
            else
                InstallPC();

            Container.BindInterfacesAndSelfTo<CameraController>().AsSingle().NonLazy();
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
        }
    }



}

