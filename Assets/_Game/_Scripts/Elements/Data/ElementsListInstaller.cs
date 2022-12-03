using UnityEngine;
using Zenject;

namespace Game
{
    public class ElementsListInstaller : MonoInstaller
    {
        [SerializeField] private ElementsListSO _elementsListSO;
        public override void InstallBindings()
        {
            Container.Bind<ElementsListSO>().FromInstance(_elementsListSO).AsSingle();
        }
    }
}