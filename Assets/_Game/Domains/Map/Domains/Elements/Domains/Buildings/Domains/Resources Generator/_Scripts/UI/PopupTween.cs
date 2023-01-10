using DG.Tweening;
using UnityEngine;

namespace Game.Map.Element.Building.Resources.UI
{
    public class PopupTween : MonoBehaviour
    {
        [SerializeField] private CanvasActivator _canvasActivator;
        [Space]
        [SerializeField] private CanvasGroup _popup;
        private Tween _scaleTween;
        private Tween _fadeTween;

        private void Awake()
        {
            _scaleTween = _popup.transform
                .DOScale(1f, 0.1f)
                .SetEase(Ease.OutCubic)
                .From(0f)
                .OnPlay(Enable)
                .OnRewind(Disable)
                .SetAutoKill(false)
                .SetLink(gameObject);

            _fadeTween = _popup
                .DOFade(1f, 0.15f)
                .SetEase(Ease.InCubic)
                .From(0f)
                .SetAutoKill(false)
                .SetLink(gameObject);
        }


        public void AutoActivate(bool isShow)
        {
            if (isShow)
                Show();
            else
                Hide();
        }
        public void Show()
        {
            _scaleTween.Restart();
            _fadeTween.Restart();
        }
        public void Hide()
        {
            _scaleTween.SmoothRewind();
            _fadeTween.SmoothRewind();
        }
        public void ForceHide()
        {
            Disable();
        }

        private void Disable()
        {
            _canvasActivator.Deactivate();
        }
        private void Enable()
        {
            _canvasActivator.Activate();
        }
    }
}
