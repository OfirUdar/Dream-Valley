using System;

namespace Game
{
    public interface IDateTimer
    {
        public event Action Ticking;
        public event Action Started;
        public event Action Finished;

        public void SetTime(DateTime targetDateTime);
        public void SetTime(TimeSpan targetTime);
        public void Start();
        public void Stop();
        public void Finish();

        public float GetCurrent();
        public float GetTarget();
        public float GetNormalizedTime();
    }
}