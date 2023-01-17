using UnityEngine;
using Zenject;

namespace Game.Map.Element.LevelSystem
{
    public class LevelSystemInstaller : MonoInstaller
    {
        [SerializeField] private LevelsElementVisualHandler _elementVisualHandler;
        [SerializeField] private LevelsListSO _levels;

        public override void InstallBindings()
        {
            Container.Bind<LevelsListSO>().FromInstance(_levels);

            Container.BindInterfacesTo<LevelManager>().AsSingle().NonLazy();

            Container.Bind<ILevelsElementVisualHandler>().FromInstance(_elementVisualHandler);

        }

    }
}