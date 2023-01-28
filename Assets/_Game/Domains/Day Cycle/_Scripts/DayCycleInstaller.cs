using UnityEngine;
using Zenject;

namespace Game.DayCycle
{
    public class DayCycleInstaller : MonoInstaller
    {
        [SerializeField] private Light _light;
        [Space]
        [SerializeField] private DayCycleData _dayCycleData;

        public override void InstallBindings()
        {
            Container.Bind<Light>().FromInstance(_light);
           // Container.Bind<Camera>().FromInstance(_camera);
            Container.Bind<DayCycleData>().FromInstance(_dayCycleData);

            Container.BindInterfacesTo<DayCycleManager>().AsSingle().NonLazy();
        }
    }
}
