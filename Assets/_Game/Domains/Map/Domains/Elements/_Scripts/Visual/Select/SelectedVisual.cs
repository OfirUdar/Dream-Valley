using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Map.Element
{
    public class SelectedVisual : MonoBehaviour, ISelectVisual
    {
        [SerializeField] private Transform _gfx;
        [SerializeField] private float _selectedOpacity = 0.5f;
        [SerializeField] private float _duration = 1f;

        public UnityEvent<bool> SelectionChanged;

        private Renderer[] _renderers;

        private void Awake()
        {
            RefreshGFX();
        }

        public void RefreshGFX()
        {
            StartCoroutine(RefreshGFXCoroutine());
        }

        private IEnumerator RefreshGFXCoroutine()
        {
            yield return null;

            _renderers = _gfx.GetComponentsInChildren<Renderer>(true);
        }

        public void Select()
        {
            _gfx.transform.DOPunchScale(Vector3.one * 0.1f, 0.1f).Play();
            foreach (var renderer in _renderers)
            {
                var material = renderer.material;

                material
                    .DOFade(_selectedOpacity, _duration)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.OutSine)
                    .SetLink(renderer.gameObject)
                    .Play();
            }

            SelectionChanged?.Invoke(true);
        }
        public void Unselect()
        {
            foreach (var renderer in _renderers)
            {
                if (renderer == null)
                    return;

                renderer.material.DOPause();
                renderer.material.DORewind();
                renderer.material.DOKill();
            }

            SelectionChanged?.Invoke(false);
        }

    }
}