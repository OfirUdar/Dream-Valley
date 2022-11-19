using System;

namespace Game
{
    public static class MainUIEventAggregator
    {
        public static event Action ShowRequested;
        public static event Action HideRequested;
        public static event Action ForceHideRequested;

        public static void Show()
        {
            ShowRequested?.Invoke();
        }
        public static void Hide()
        {
            HideRequested?.Invoke();
        }
        public static void ForceHide()
        {
            ForceHideRequested?.Invoke();
        }
    }
}