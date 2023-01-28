using UnityEngine;
using Zenject;

namespace Game.Map.Element.Obstcales
{
    public class ObstcalesSpawnerInstaller : MonoInstaller
    {
        [SerializeField] private ObstcalesListSO _treesListSO;

        public override void InstallBindings()
        {
            Container.Bind<ObstcalesListSO>().FromInstance(_treesListSO);

            Container.BindInterfacesTo<ObstcalesRandomSpawner>().AsSingle();
        }
    }
}
