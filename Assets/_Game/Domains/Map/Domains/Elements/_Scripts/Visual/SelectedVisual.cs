using DG.Tweening;
using UnityEngine;

namespace Game.Map.Element
{
    public class SelectedVisual : MonoBehaviour, ISelectVisual
    {
        [SerializeField] private Transform _gfx;
        [SerializeField] private float _selectedOpacity = 0.5f;
        [SerializeField] private float _duration = 1f;

        private Renderer[] _renderers;

        private void Awake()
        {
            _renderers = _gfx.GetComponentsInChildren<Renderer>(true);
        }

        public void Select()
        {
            _gfx.transform.DOPunchScale(Vector3.one*0.1f,0.1f).Play();
            foreach (var renderer in _renderers)
            {
                var material = renderer.material;

                material
                    .DOFade(_selectedOpacity, _duration)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.OutSine).Play();
            }
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
        }

    }
    public interface ISelectVisual
    {
        public void Select();
        public void Unselect();
    }
}