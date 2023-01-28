using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class MainUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _main;
        [SerializeField] private RectTransform _resourcesContainer;
        [SerializeField] private RectTransform _buildButton;

        private Sequence _hideTweenSequence;


        private void Awake()
        {
            _hideTweenSequence = DOTween.Sequence();
            var tweenResources = _resourcesContainer.DOAnchorPosX(100f, 0.2f)
                   .SetEase(Ease.InBack);
            var tweenBuild = _buildButton.DOAnchorPosX(100f, 0.2f)
                   .SetEase(Ease.InBack);

            _hideTweenSequence
                .Append(tweenResources)
                .Append(tweenBuild)
                .SetAutoKill(false)
                .OnPlay(Enable)
                 .OnComplete(Disable);
        }

        private void OnEnable()
        {
            MainUIEventAggregator.ShowRequested += Show;
            MainUIEventAggregator.HideRequested += Hide;
            MainUIEventAggregator.ForceHideRequested += ForceHide;
        }
        private void OnDisable()
        {
            MainUIEventAggregator.ShowRequested -= Show;
            MainUIEventAggregator.HideRequested -= Hide;
            MainUIEventAggregator.ForceHideRequested -= ForceHide;
        }
        public void Show()
        {
            _hideTweenSequence.SmoothRewind();
        }

        public void Hide()
        {
            _hideTweenSequence.Restart();
        }

        public void ForceHide()
        {
            Disable();
        }

        private void Disable()
        {
            _main.SetActive(false);
        }
        private void Enable()
        {
            _main.SetActive(true);
        }

    }

}
