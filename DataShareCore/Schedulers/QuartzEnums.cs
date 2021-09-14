// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataShareCore.Schedulers
{
    public static class QuartzEnums
    {
        public enum Interval
        {
            TimeSpan = 0,
            Cron = 1,
            Seconds = 2,
            Minutes = 3,
            Hours = 4,
        }
    }
}
