// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Quartz;
using System;
using System.Collections.Generic;

namespace DataShareCore.Schedulers
{
    public static class QuartzUtils
    {
        public static ITrigger? CreateTrigger(TimeSpan timeSpan, bool repeatForever) =>
            new QuartzTriggerEntity(timeSpan, repeatForever).Trigger;

        public static ITrigger? CreateTrigger(string cronExpression, bool repeatForever) =>
            new QuartzTriggerEntity(cronExpression, repeatForever).Trigger;

        public static ITrigger? CreateTrigger(QuartzEnums.Interval interval, int length, bool repeatForever) =>
            new QuartzTriggerEntity(interval, length, repeatForever).Trigger;

        public static IJobDetail? CreateJobDetail(Action action, string jobName)
        {
            IJobDetail? jobDetail;
            QuartzJobEntity.Actions.Add(action, jobName);
            JobBuilder jobBuilder = JobBuilder
                .Create<QuartzJobEntity>()
                .WithIdentity(jobName);
            jobDetail = jobBuilder.Build();
            return jobDetail;
        }

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

            public static string EverySeconds(int seconds)
            {
                if (seconds == 0)
                    throw new ArgumentException($"Use {nameof(seconds)} with value more then 0!");
                return $"0/{seconds} * * ? * *";
            }

            public static string EveryMinutes(int minutes)
            {
                if (minutes == 0)
                    throw new ArgumentException($"Use {nameof(minutes)} with value more then 0!");
                return $"0 0/{minutes} * ? * *";
            }

            public static string EveryHours(int hours)
            {
                if (hours == 0)
                    throw new ArgumentException($"Use {nameof(hours)} with value more then 0!");
                return $"0 0 0/{hours} ? * *";
            }

            public static string EveryDay() => $"0 0 0 ? * *";
        }
    }
}
