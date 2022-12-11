﻿using Zenject;

namespace Game.Map
{
    public class MapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MapManager>().AsSingle();
            Container.Bind<IMapSaver>().To<MapSaver>().AsSingle();
        }
    }
}