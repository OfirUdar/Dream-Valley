using UnityEngine;
using Zenject;

namespace Game.Map.Element.Obstcales
{
    public class ObstcaleInstaller : MonoInstaller
    {
        [SerializeField] private ObstacleDataSO _obstacleDataSO;

        public override void InstallBindings()
        {
            Container.Bind<ObstacleDataSO>().FromInstance(_obstacleDataSO);

            Container.BindInterfacesTo<ObstacleRemoveHandler>().AsSingle();
        }
    }
}
