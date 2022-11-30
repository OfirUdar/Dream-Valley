using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game
{
    public class UITimer : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private TextMeshProUGUI _amountText;

        private IDateTimer _timer;

        [SerializeField] private float seconds = 100;

        [Inject]
        public void Init(IDateTimer timer)
        {
            _timer = timer;
            _timer.Start();
        }

        private void OnEnable()
        {
            _timer.Started += OnTimerStarted;
            _timer.Ticking += OnTimerTicking;
        }
        private void OnDisable()
        {
            _timer.Started -= OnTimerStarted;
            _timer.Ticking -= OnTimerTicking;
        }

        private void OnTimerStarted()
        {

        }
        private void OnTimerTicking()
        {
            _fillImage.fillAmount = _timer.GetNormalizedTime();
            var currentTime = _timer.GetCurrent();
            var span = TimeSpan.FromSeconds(currentTime);

            _amountText.text = GetDisplayTime(span);
        }

        private string GetDisplayTime(TimeSpan span)
        {
            if (span.Days > 1)
            {
                return string.Format("{0:D2}d {1:D2}h {2:D2}m",
                  span.Days,
                  span.Hours,
                  span.Minutes);
            }
            if (span.Hours > 1)
            {
                return string.Format("{0:D2}h {1:D2}m {2:D2}s",
                  span.Hours,
                  span.Minutes,
                  span.Seconds);
            }
            if (span.Minutes > 1)
            {
                return string.Format("{0:D2}m {1:D2}s",
                  span.Minutes,
                  span.Seconds);
            }
            if (span.Seconds > 1)
            {
                return string.Format("{0:D2}s",
                  span.Seconds);
            }
            return null;
        }


    }

}