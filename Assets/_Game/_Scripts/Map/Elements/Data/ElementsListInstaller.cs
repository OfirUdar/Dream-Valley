using UnityEngine;
using Zenject;

namespace Game
{
    public class ElementsListInstaller : MonoInstaller
    {
        [SerializeField] private ElementsListSO _initElementListSO;
        [SerializeField] private ElementsListSO _elementListSO;
        public override void InstallBindings()
        {
            Container.Bind<ElementsListSO>().FromInstance(_elementListSO);
            Container.Bind<ElementsListSO>().WithId("Initalize").FromInstance(_initElementListSO);
        }
    }
}