using System;
using System.Collections;
using UnityEngine;

namespace Game.Map.Element
{
    public class LongPressEvent : MonoBehaviour
    {
        private const float MIN_TIME_PRESS = 0.3f;

        private Coroutine _timerCoroutine;
        public event Action PressExecuted;

        private void OnMouseDown()
        {
            if (_timerCoroutine != null)
                StopCoroutine(_timerCoroutine);

            _timerCoroutine = StartCoroutine(TimerCountdownCoroutine());
        }
        private void OnMouseUpAsButton()
        {
            if (_timerCoroutine != null)
                StopCoroutine(_timerCoroutine);
        }

        private IEnumerator TimerCountdownCoroutine()
        {
            var timer = MIN_TIME_PRESS;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;
            }

            PressExecuted?.Invoke();
        }
    }
}