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

        }

        private void InstallMobile()
        {
            Container.BindInterfacesAndSelfTo<TouchCameraMove>()
                   .AsSingle()
                  .WithArguments(_camera.transform)
                  .NonLazy();

            Container.BindInterfacesAndSelfTo<TouchCameraZoom>()
               .AsSingle()
               .WithArguments(_camera)
               .NonLazy();
        }
        private void InstallPC()
        {
            Container.BindInterfacesAndSelfTo<MouseCameraMove>()
                     .AsSingle()
                    .WithArguments(_camera.transform)
                    .NonLazy();

            Container.BindInterfacesAndSelfTo<MouseCameraZoom>()
               .AsSingle()
               .WithArguments(_camera)
               .NonLazy();
        }
    }



}

