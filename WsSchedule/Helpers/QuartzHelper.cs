// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsSchedule.Enums;
using WsSchedule.Models;
using WsSchedule.Utils;

namespace WsSchedule.Helpers;

public class QuartzHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static QuartzHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static QuartzHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private StdSchedulerFactory? _factory;
    private StdSchedulerFactory Factory => _factory ??= new();
    private IScheduler? _scheduler;
    private IScheduler Scheduler => _scheduler ??= Factory.GetScheduler().Result;

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

    private void Start()
    {
        if (!Scheduler.IsStarted)
            Scheduler.Start();
    }

    public void Close()
    {
        if (!Scheduler.IsShutdown)
            Scheduler.Shutdown();
    }

    private IJobDetail? CreateJobDetail(QuartzEnums.Interval interval, string cronExpression, Action action, string jobName)
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
            else if (cronExpression == QuartzUtils.CronExpression.EveryMinutes10())
                interval = QuartzEnums.Interval.Minutes10;
            else if (cronExpression == QuartzUtils.CronExpression.EveryHours())
                interval = QuartzEnums.Interval.Hours;
            else if (cronExpression == QuartzUtils.CronExpression.EveryDays())
                interval = QuartzEnums.Interval.Days;
        }

        switch (interval)
        {
            case QuartzEnums.Interval.Cron:
                QuartzJobEverySecondsModel.Actions.Add(action);
                jobBuilder = JobBuilder
                    .Create<QuartzJobModel>()
                    .WithIdentity(jobName);
                break;
            case QuartzEnums.Interval.Seconds:
                QuartzJobEverySecondsModel.Actions.Add(action);
                jobBuilder = JobBuilder
                    .Create<QuartzJobEverySecondsModel>()
                    .WithIdentity(jobName);
                break;
            case QuartzEnums.Interval.Seconds10:
                QuartzJobEverySeconds10Model.Actions.Add(action);
                jobBuilder = JobBuilder
                    .Create<QuartzJobEverySeconds10Model>()
                    .WithIdentity(jobName);
                break;
            case QuartzEnums.Interval.Minutes:
                QuartzJobEveryMinutesModel.Actions.Add(action);
                jobBuilder = JobBuilder
                    .Create<QuartzJobEveryMinutesModel>()
                    .WithIdentity(jobName);
                break;
            case QuartzEnums.Interval.Minutes10:
                QuartzJobEveryMinutes10Model.Actions.Add(action);
                jobBuilder = JobBuilder
                    .Create<QuartzJobEveryMinutes10Model>()
                    .WithIdentity(jobName);
                break;
            case QuartzEnums.Interval.Hours:
                QuartzJobEveryHoursModel.Actions.Add(action);
                jobBuilder = JobBuilder
                    .Create<QuartzJobEveryHoursModel>()
                    .WithIdentity(jobName);
                break;
            case QuartzEnums.Interval.Days:
                QuartzJobEveryDaysModel.Actions.Add(action);
                jobBuilder = JobBuilder
                    .Create<QuartzJobEveryDaysModel>()
                    .WithIdentity(jobName);
                break;
            case QuartzEnums.Interval.TimeSpan:
                break;
        }
        if (jobBuilder is not null)
            jobDetail = jobBuilder.Build();
        return jobDetail;
    }

    #endregion
}