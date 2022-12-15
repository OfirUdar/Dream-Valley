using System;

namespace Game.Map.Element.Building
{
    [Serializable]
    public struct TimerSaveData
    {
        public string StartTime;
    }

    public interface ITimerLoader
    {
        public ITimerLoader Load(string data);

        public int GetCyclesAmount(int targetSeconds);
        public int GetRemainingSeconds(int targetSeconds);

    }
}