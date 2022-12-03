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

        public IDateTimer SetTime(DateTime targetDateTime)
        {
            _targetDateTime = targetDateTime;

            _targetTimeSeconds = (float)(_targetDateTime - DateTime.Now).TotalSeconds;
            _timerSeconds = _targetTimeSeconds;

            return this;

        }
        public IDateTimer SetTime(TimeSpan targetTimeSpan)
        {
            _targetDateTime = DateTime.Now + targetTimeSpan;

            _targetTimeSeconds = (float)targetTimeSpan.TotalSeconds;
            _timerSeconds = _targetTimeSeconds;

            return this;

        }
        public IDateTimer Start()
        {
            _isTicking = true;
            Started?.Invoke();
            return this;

        }
        public IDateTimer Stop()
        {
            _isTicking = false;
            return this;

        }
        public IDateTimer Finish()
        {
            Stop();
            _timerSeconds = 0;
            Finished?.Invoke();
            return this;
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