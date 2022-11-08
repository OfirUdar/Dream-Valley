﻿using DG.Tweening;
using UnityEngine;

namespace Game.Map.Grid.Element
{
    public class SelectedVisual : MonoBehaviour
    {
        [SerializeField] private Transform _gfx;
        [SerializeField] private float _selectedOpacity = 0.5f;
        [SerializeField] private float _duration = 1f;

        private Renderer[] _renderers;

        private void Awake()
        {
            _renderers = _gfx.GetComponentsInChildren<Renderer>(true);
        }

        public void OnSelectionChanged(bool isSelected)
        {
            if (isSelected)
            {
                foreach (var renderer in _renderers)
                {
                    var material = renderer.material;

                    material
                        .DOFade(_selectedOpacity, _duration)
                        .SetLoops(-1, LoopType.Yoyo)
                        .SetEase(Ease.OutSine);
                }
            }
            else
            {
                foreach (var renderer in _renderers)
                {
                    renderer.material.DOKill();
                    renderer.material.DOFade(1f, 0f);
                }
            }


        }
    }
}