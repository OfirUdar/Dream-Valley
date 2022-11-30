using System;
using Udar;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Timer : IDateTimer, ITickable
    {
        public event Action Ticking;
        public event Action Started;
        public event Action Finished;

        private bool _isTicking = false;
        private float _timerSeconds;
        private float _targetTimeSeconds;

        private DateTime _targetDateTime;

        public void Tick()
        {
            if (!_isTicking)
                return;

            if (_timerSeconds <= 0)
            {
                Finish();
                return;
            }
            _timerSeconds -= Time.unscaledDeltaTime;

            Ticking?.Invoke();
        }

        public void SetTime(DateTime targetDateTime)
        {
            _targetDateTime = targetDateTime;

            _targetTimeSeconds = (float)(_targetDateTime - DateTime.Now).TotalSeconds;
            _timerSeconds = _targetTimeSeconds;
        }
        public void SetTime(TimeSpan targetTimeSpan)
        {
            _targetDateTime = DateTime.Now + targetTimeSpan;

            _targetTimeSeconds = (float)targetTimeSpan.TotalSeconds;
            _timerSeconds = _targetTimeSeconds;
        }
        public void Start()
        {
            _isTicking = true;
            Started?.Invoke();
        }
        public void Stop()
        {
            _isTicking = false;
        }
        public void Finish()
        {
            Stop();
            _timerSeconds = 0;
            Finished?.Invoke();
        }

        public float GetCurrent()
        {
            return _timerSeconds;
        }
        public float GetTarget()
        {
            return _targetTimeSeconds;
        }
        public float GetNormalizedTime()
        {
            return _timerSeconds / _targetTimeSeconds;
        }


    }


}