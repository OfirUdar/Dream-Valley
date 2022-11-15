using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PanelScreen : MonoBehaviour
    {
        private const float DURATION = 0.15f;

        [SerializeField] private Image _backgroundImage;
        [SerializeField] private RectTransform _window;

        private Tween _windowTween;
        private Tween _backgroundTween;

        private void Awake()
        {
            _windowTween = _window.DOScale(0f, DURATION).
               SetEase(Ease.OutBack).OnComplete(SetDisable).SetAutoKill(false);

            _backgroundTween = _backgroundImage.DOFade(0f, DURATION).
                SetEase(Ease.InOutSine).SetAutoKill(false);
        }
        private void SetEnable()
        {
            gameObject.SetActive(true);
        }
        private void SetDisable()
        {
            gameObject.SetActive(false);
        }
        public void Show()
        {

            SetEnable();

            _windowTween.PlayBackwards();
            _backgroundTween.PlayBackwards();
        }
        public void Hide()
        {
            _windowTween.Restart();
            _backgroundTween.Restart();
        }



    }
}