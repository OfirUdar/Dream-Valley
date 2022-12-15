using System;

namespace Game
{
    public interface IDateTimer
    {
        public event Action Ticking;
        public event Action Started;
        public event Action Finished;

        public IDateTimer SetTime(DateTime targetDateTime);
        public IDateTimer SetTime(TimeSpan targetTimeSpan);
        public IDateTimer SetTime(TimeSpan currentTime,TimeSpan targetTimeSpan);
        public IDateTimer Start();
        public IDateTimer Stop();
        public IDateTimer Finish();

        public float GetCurrent();
        public float GetTarget();
        public float GetNormalizedTime();
    }
}