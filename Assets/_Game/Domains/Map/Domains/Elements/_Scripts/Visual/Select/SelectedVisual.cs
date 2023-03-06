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
        public UnityEvent SelectedChanged;
        public UnityEvent UnselectedChanged;

        private Renderer[] _renderers;

        private Tween[] _fadeTweens;


        private void Awake()
        {
            RefreshGFX();
        }

       
        private IEnumerator RefreshGFXCoroutine()
        {
            yield return null;

            _renderers = _gfx.GetComponentsInChildren<Renderer>(true);

            _fadeTweens = new Tween[_renderers.Length];
            for (int i = 0; i < _renderers.Length; i++)
            {
                var material = _renderers[i].material;

                _fadeTweens[i] = DOVirtual.Float(0, 0.3f, _duration, OnFadeUpdate)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.OutSine)
                    .SetAutoKill(false)
                    .SetLink(_renderers[i].gameObject);
            }

        }

        private void OnFadeUpdate(float value)
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                var material = _renderers[i].material;
                material.SetVector("_EmissionColor", Color.white * value);
            }
        }
        public void RefreshGFX()
        {
            StartCoroutine(RefreshGFXCoroutine());
        }

        public void Select()
        {
            _gfx.transform.DOPunchScale(Vector3.one * 0.15f, 0.15f).Play();

            for (int i = 0; i < _fadeTweens.Length; i++)
            {
                _fadeTweens[i].Restart();
            }
            SelectionChanged?.Invoke(true);
            SelectedChanged?.Invoke();
        }
        public void Unselect()
        {
            for (int i = 0; i < _fadeTweens.Length; i++)
            {
                _fadeTweens[i].Rewind();
            }
            SelectionChanged?.Invoke(false);
            UnselectedChanged?.Invoke();
        }

    }
}