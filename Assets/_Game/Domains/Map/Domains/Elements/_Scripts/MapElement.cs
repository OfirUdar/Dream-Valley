﻿using System;
using UnityEngine;

namespace Game.Map.Element
{
    public class MapElement : IMapElement
    {
        private readonly IPlaceable _placer;
        private readonly ISelectable _selector;
        private readonly IDraggable _dragger;
        private readonly IPlaceApprover _placeApprover;
        private readonly GameObject _elementObject;



        public MapElement(IPlaceable placer,
            ISelectable selector,
            IDraggable dragger,
            IPlaceApprover placeApprover,
            GameObject elementObject)
        {
            _placer = placer;
            _selector = selector;
            _dragger = dragger;
            _placeApprover = placeApprover;
            _elementObject = elementObject;
        }


        #region Place
        public int Width => _placer.Width;

        public int Height => _placer.Height;

        public Vector3 Position { get => _placer.Position; set => _placer.Position = value; }
        #endregion

        #region Selection
        public bool IsSelected => _selector.IsSelected;


        public void Select()
        {
            _selector.Select();
        }
        public void Unselect()
        {
            _selector.Unselect();
        }
        #endregion

        #region Drag
        public void StartDrag()
        {
            _dragger.StartDrag();
        }
        public void EndDrag(bool hasPlaced)
        {
            _dragger.EndDrag(hasPlaced);
        }
        public void OnDrag(bool canPlace)
        {
            _dragger.OnDrag(canPlace);
        }
        #endregion

        #region Place Approver
        public IPlaceApprover PlaceApprover => _placeApprover;

        #endregion


        public void Destroy()
        {
            GameObject.Destroy(_elementObject);
        }


    }
}