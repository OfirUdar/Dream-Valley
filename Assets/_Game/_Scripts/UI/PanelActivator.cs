using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PanelActivator : MonoBehaviour
    {
        private const float DURATION = 0.15f;

        [SerializeField] private GameObject _panelToActivate;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private RectTransform _window;

        private Tween _windowTween;
        private Tween _backgroundTween;

        private void Awake()
        {
            _windowTween = _window.DOScale(0f, DURATION).
               SetEase(Ease.OutCubic).OnComplete(SetDisable).SetAutoKill(false);

            _backgroundTween = _backgroundImage.DOFade(0f, DURATION).
                SetEase(Ease.InOutSine).SetAutoKill(false);

            _windowTween.Complete(false);
            _backgroundTween.Complete(false);
        }
        private void SetEnable()
        {
            _panelToActivate.SetActive(true);
        }
        private void SetDisable()
        {
            _panelToActivate.SetActive(false);
        }
        public void Show()
        {
            SetEnable();
            _windowTween.SmoothRewind();
            _backgroundTween.SmoothRewind();
        }
        public void Hide()
        {
            _windowTween.Restart();
            _backgroundTween.Restart();
        }

        public void ForceHide()
        {
            SetDisable();
        }


    }
}