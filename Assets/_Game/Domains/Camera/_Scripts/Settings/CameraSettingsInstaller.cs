using UnityEngine;
using Zenject;

namespace Game.Camera
{
    [CreateAssetMenu(fileName = "Camera Settings", menuName = "Camera/Settings", order = 0)]
    public class CameraSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private MoveSettings _moveSettings;
        [SerializeField] private ZoomSettings _zoomSettings;

        public override void InstallBindings()
        {
            Container.Bind<MoveSettings>().FromInstance(_moveSettings).AsSingle();
            Container.Bind<ZoomSettings>().FromInstance(_zoomSettings).AsSingle();
        }
    }
}
