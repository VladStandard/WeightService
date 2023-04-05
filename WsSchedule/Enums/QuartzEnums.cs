// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsSchedule.Enums;

public static class QuartzEnums
{
    public enum Interval
    {
        TimeSpan,
        Cron,
        Seconds,
        Seconds10,
        Minutes,
        Minutes10,
        Hours,
        Days,
    }
}