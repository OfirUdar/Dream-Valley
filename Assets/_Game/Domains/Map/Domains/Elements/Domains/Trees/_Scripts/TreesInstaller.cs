using UnityEngine;
using Zenject;

namespace Game.Map.Element.Trees
{
    public class TreesInstaller : MonoInstaller
    {
        [SerializeField] private TreesListSO _treesListSO;

        public override void InstallBindings()
        {
            Container.Bind<TreesListSO>().FromInstance(_treesListSO);

            Container.BindInterfacesTo<TreeRandomSpawner>().AsSingle();
        }
    }

}
