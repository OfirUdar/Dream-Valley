using UnityEngine;
using Zenject;

namespace Game
{
    public class GridInstaller : MonoInstaller
    {
        [SerializeField] private PlacementSO buildingSO;
        [Space]
        [SerializeField] private GridSettingsSO _settings;

        public override void InstallBindings()
        {
            Container.Bind<GridSettings>()
                .FromInstance(_settings.Settings).AsSingle();


            Container.Bind<IGrid>().To<Grid>()
                .AsSingle().NonLazy();

            Container.Bind(typeof(ISelectionManager),typeof(ITickable))
                .To<GridSelectionManager>()
                .AsSingle().NonLazy();


            Container.BindFactory
                <Object, PlacementBehaviour, PlacementBehaviour.Factory>()
                .FromFactory<PrefabFactory<PlacementBehaviour>>();

            UnityEngine.SceneManagement.SceneManager.LoadScene("MainUI", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }


    }
}
