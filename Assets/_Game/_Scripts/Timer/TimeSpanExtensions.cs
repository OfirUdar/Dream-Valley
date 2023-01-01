using System;

namespace Game
{
    public static class TimeSpanExtensions
    {
        public static string ToDisplayTime(this TimeSpan span)
        {
            if (span.Days >= 1)
            {
                return string.Format("{0:D2}d {1:D2}h {2:D2}m",
                  span.Days,
                  span.Hours,
                  span.Minutes);
            }
            if (span.Hours >= 1)
            {
                return string.Format("{0:D2}h {1:D2}m {2:D2}s",
                  span.Hours,
                  span.Minutes,
                  span.Seconds);
            }
            if (span.Minutes >= 1)
            {
                return string.Format("{0:D2}m {1:D2}s",
                  span.Minutes,
                  span.Seconds);
            }

            return string.Format("{0:D2}s",
              span.Seconds);

        }
    }

}