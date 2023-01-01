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
            _fillImage.fillAmount = 1 - _timer.GetNormalizedTime();
            var currentTime = _timer.GetCurrent();
            var span = TimeSpan.FromSeconds(currentTime);

            _amountText.text = span.ToDisplayTime();
        }

      


    }

}