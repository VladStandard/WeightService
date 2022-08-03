// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Quartz;
using Quartz.Impl;
using System;
using System.Threading;

namespace DataCore.Schedulers
{
    public class QuartzHelper : IDisposable
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static QuartzHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static QuartzHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields, properties, constructor

        public StdSchedulerFactory? _factory = null;
        public StdSchedulerFactory Factory
        {
            get
            {
                if (_factory == null)
                    _factory = new StdSchedulerFactory();
                return _factory;
            }
        }
        public IScheduler? _scheduler = null;
        public IScheduler Scheduler
        {
            get
            {
                if (_scheduler == null)
                    _scheduler = Factory.GetScheduler().Result;
                return _scheduler;
            }
        }

        #endregion

        #region Public and private methods

        public void AddJob(string cronExpression, Action action, string jobName, string triggerName, string triggerGroup)
        {
            QuartzEnums.Interval interval = QuartzEnums.Interval.Cron;
            int length = 0;
            TimeSpan timeSpan = new(0);
            bool repeatForever = false;

            ITrigger? trigger = interval switch
            {
                QuartzEnums.Interval.TimeSpan => QuartzUtils.CreateTrigger(triggerName, triggerGroup, timeSpan, repeatForever),
                QuartzEnums.Interval.Cron => QuartzUtils.CreateTrigger(triggerName, triggerGroup, cronExpression, repeatForever),
                _ => QuartzUtils.CreateTrigger(triggerName, triggerGroup, interval, length, repeatForever),
            };
            if (trigger == null)
                return;

            // Job.
            IJobDetail? jobDetail = CreateJobDetail(interval, cronExpression, action, jobName);
            if (jobDetail == null)
                return;

            Console.WriteLine($"{nameof(jobDetail)} is added with name: {jobName}");
            Start();
            Scheduler.ScheduleJob(jobDetail, trigger);
        }

        public void Dispose()
        {
            Close();
        }

        public void Start()
        {
            if (!Scheduler.IsStarted)
                Scheduler.Start();
        }

        public void Close()
        {
            if (!Scheduler.IsShutdown)
                Scheduler.Shutdown();
        }

        public IJobDetail? CreateJobDetail(QuartzEnums.Interval interval, string cronExpression, Action action, string jobName)
        {
            IJobDetail? jobDetail = null;
            JobBuilder? jobBuilder = null;
            if (interval == QuartzEnums.Interval.Cron)
            {
                if (cronExpression == QuartzUtils.CronExpression.EverySeconds())
                    interval = QuartzEnums.Interval.Seconds;
                else if (cronExpression == QuartzUtils.CronExpression.EverySeconds10())
                    interval = QuartzEnums.Interval.Seconds10;
                else if (cronExpression == QuartzUtils.CronExpression.EveryMinutes())
                    interval = QuartzEnums.Interval.Minutes;
                else if (cronExpression == QuartzUtils.CronExpression.EveryHours())
                    interval = QuartzEnums.Interval.Hours;
                else if (cronExpression == QuartzUtils.CronExpression.EveryDays())
                    interval = QuartzEnums.Interval.Days;
            }
            switch (interval)
            {
                case QuartzEnums.Interval.Cron:
                    QuartzJobEverySecondsEntity.Actions.Add(action);
                    jobBuilder = JobBuilder
                        .Create<QuartzJobEntity>()
                        .WithIdentity(jobName);
                    break;
                case QuartzEnums.Interval.Seconds:
                    QuartzJobEverySecondsEntity.Actions.Add(action);
                    jobBuilder = JobBuilder
                        .Create<QuartzJobEverySecondsEntity>()
                        .WithIdentity(jobName);
                    break;
                case QuartzEnums.Interval.Seconds10:
                    QuartzJobEverySeconds10Entity.Actions.Add(action);
                    jobBuilder = JobBuilder
                        .Create<QuartzJobEverySeconds10Entity>()
                        .WithIdentity(jobName);
                    break;
                case QuartzEnums.Interval.Minutes:
                    QuartzJobEveryMinutesEntity.Actions.Add(action);
                    jobBuilder = JobBuilder
                        .Create<QuartzJobEveryMinutesEntity>()
                        .WithIdentity(jobName);
                    break;
                case QuartzEnums.Interval.Hours:
                    QuartzJobEveryHoursEntity.Actions.Add(action);
                    jobBuilder = JobBuilder
                        .Create<QuartzJobEveryHoursEntity>()
                        .WithIdentity(jobName);
                    break;
                case QuartzEnums.Interval.Days:
                    QuartzJobEveryDaysEntity.Actions.Add(action);
                    jobBuilder = JobBuilder
                        .Create<QuartzJobEveryDaysEntity>()
                        .WithIdentity(jobName);
                    break;
                case QuartzEnums.Interval.TimeSpan:
                    break;
            }
            if (jobBuilder != null)
                jobDetail = jobBuilder.Build();
            return jobDetail;
        }

        #endregion
    }
}
