using System;
using System.Collections.Generic;
using System.IO;
using Udar;
using UnityEngine;
using Zenject;

namespace Game.Map
{
    public class MapManager : IInitializable, ILateDisposable
    {
        private readonly IMapSaver _mapSaver;
        private readonly IMapGrid _grid;

        public MapManager(IMapSaver mapSaver,IMapGrid grid)
        {
            _mapSaver = mapSaver;
            _grid = grid;
        }

        public void Initialize()
        {
            _mapSaver.LoadAll();
            _grid.ElementChanged += OnElementChanged;
            _grid.ElementRemoved += OnElementRemoved;
        }
        public void LateDispose()
        {
            _grid.ElementChanged -= OnElementChanged;
            _grid.ElementRemoved -= OnElementRemoved;
        }

        private void OnElementChanged(IMapElement element)
        {
            if (element == null)
                return;


            _mapSaver.SaveElement(element);

            //_saveManager.Save(this);
        }
        private void OnElementRemoved(IMapElement element)
        {
            _mapSaver.DeleteElement(element);

            //_mapElements.Remove(element);
            // _saveManager.Save(this);

        }
       
    }

}
