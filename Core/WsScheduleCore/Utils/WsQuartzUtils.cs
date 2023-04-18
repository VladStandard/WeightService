// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// https://www.freeformatter.com/cron-expression-generator-quartz.html

using WsScheduleCore.Enums;
using WsScheduleCore.Models;

namespace WsScheduleCore.Utils;

public static class WsQuartzUtils
{
    public static ITrigger? CreateTrigger(string name, string group, TimeSpan timeSpan, bool repeatForever) =>
        new WsQuartzTriggerModel(name, group, timeSpan, repeatForever).Trigger;

    public static ITrigger? CreateTrigger(string name, string group, string cronExpression, bool repeatForever) =>
        new WsQuartzTriggerModel(name, group, cronExpression, repeatForever).Trigger;

    public static ITrigger? CreateTrigger(string name, string group, WsQuartzInterval interval, int length, bool repeatForever) =>
        new WsQuartzTriggerModel(name, group, interval, length, repeatForever).Trigger;

    public static class CronExpression
    {
        public static string AfterSeconds(int seconds)
        {
            DateTime dt = DateTime.Now.AddSeconds(seconds);
            return $"{dt.Second} {dt.Minute} {dt.Hour} ? * *";
        }

        public static string AfterMinutes(int minutes)
        {
            DateTime dt = DateTime.Now.AddMinutes(minutes);
            return $"{dt.Second} {dt.Minute} {dt.Hour} ? * *";
        }

        public static string AfterHours(int hours)
        {
            DateTime dt = DateTime.Now.AddMinutes(hours);
            return $"{dt.Second} {dt.Minute} {dt.Hour} ? * *";
        }

        public static string EverySeconds(int seconds = 0) => seconds == 0 ? "* * * ? * *" : $"*/{seconds} * * ? * *";

        public static string EverySeconds10() => "*/10 * * ? * *";

        public static string EveryMinutes(int minutes = 0) => minutes == 0 ? "0 * * ? * *" : $"0 */{minutes} * ? * *";

        public static string EveryMinutes10(int minutes = 0) => minutes == 0 ? "0 0/10 * ? * *" : $"0 0/{minutes} * ? * *";

        public static string EveryHours(int hours = 0) => hours == 0 ? "0 0 * ? * *" : $"0 0 */{hours} ? * *";

        public static string EveryDays(int days = 0) => days == 0 ? "0 0 0 ? * *" : $"0 0 0 */{days} * ? *";
    }
}