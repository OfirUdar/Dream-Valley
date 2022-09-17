using UnityEngine;
using Zenject;

namespace Game
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private MoveSettings _moveSettings;
        [SerializeField] private ZoomSettings _zoomSettings;

        public override void InstallBindings()
        {
            if (Application.isMobilePlatform)
                InstallMobile();
            else
                InstallPC();

        }

        private void InstallMobile()
        {
            Container.BindInterfacesAndSelfTo<MouseCameraMove>()
                   .AsSingle()
                  .WithArguments(_camera.transform, _moveSettings)
                  .NonLazy();

            Container.BindInterfacesAndSelfTo<TouchCameraZoom>()
               .AsSingle()
               .WithArguments(_camera, _zoomSettings)
               .NonLazy();
        }
        private void InstallPC()
        {
            Container.BindInterfacesAndSelfTo<MouseCameraMove>()
                     .AsSingle()
                    .WithArguments(_camera.transform, _moveSettings)
                    .NonLazy();

            Container.BindInterfacesAndSelfTo<MouseCameraZoom>()
               .AsSingle()
               .WithArguments(_camera, _zoomSettings)
               .NonLazy();
        }
    }



}

