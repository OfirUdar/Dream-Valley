using UnityEngine;
using Zenject;

namespace Game.Map.Grid.Element
{
    public class ElementInstaller : MonoInstaller
    {
        [SerializeField] private MapElementSO _elementSO;
        [SerializeField] private MeshRenderer _editMeshRenderer;


        public override void InstallBindings()
        {
            Container.BindFactory
               <Object, MapElementBehaviour, MapElementBehaviour.Factory>()
               .FromFactory<PrefabFactory<MapElementBehaviour>>();

            Container.Bind<MapElementData>().FromInstance(_elementSO.Data);

            Container.BindInstance(_editMeshRenderer).WhenInjectedInto<AreaSizeFitter>();
            Container.Bind<AreaSizeFitter>().ToSelf().AsSingle().NonLazy();
        }
    }
}