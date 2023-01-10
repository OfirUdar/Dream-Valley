using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PanelActivator : MonoBehaviour
    {
        private const float DURATION = 0.1f;

        [SerializeField] private CanvasActivator _canvasActivator;
        [Space]
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private CanvasGroup _window;
        [Space]
        [SerializeField] private bool _isDestroyOnHide = false;

        private Tween _windowFadeTween;
        private Tween _windowScaleTween;
        private Tween _backgroundTween;

        private void Awake()
        {
            _windowFadeTween = _window.DOFade(1f, DURATION).From(0f)
                .SetEase(Ease.OutCubic)
                 .SetAutoKill(false)
                 .SetLink(gameObject);

            _windowScaleTween = _window.transform.DOScale(1f, DURATION).From(0f)
               .SetEase(Ease.OutCubic)
               .OnPlay(SetEnable)
               .OnRewind(SetDisable)
               .SetAutoKill(false)
               .SetLink(gameObject);

            _backgroundTween = _backgroundImage.DOFade(_backgroundImage.color.a, DURATION).From(0f)
                .SetEase(Ease.InOutSine)
                .SetAutoKill(false)
                .SetLink(gameObject);
        }
        private void SetEnable()
        {
            _canvasActivator.Activate();
        }
        private void SetDisable()
        {
            if (_isDestroyOnHide)
            {
                Destroy(gameObject);
                return;
            }
            _canvasActivator.Deactivate();
        }
        public void Show()
        {
            _windowFadeTween.Restart();
            _windowScaleTween.Restart();
            _backgroundTween.Restart();
        }
        public void Hide()
        {
            _windowFadeTween.SmoothRewind();
            _windowScaleTween.SmoothRewind();
            _backgroundTween.SmoothRewind();

        }

        public void ForceHide()
        {
            SetDisable();
        }


    }
}