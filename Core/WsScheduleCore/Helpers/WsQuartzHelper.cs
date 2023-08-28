using WsScheduleCore.Utils;

namespace WsScheduleCore.Helpers;

public class WsQuartzHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsQuartzHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsQuartzHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

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
        WsQuartzInterval interval = WsQuartzInterval.Cron;
        int length = 0;
        TimeSpan timeSpan = new(0);
        bool repeatForever = false;

        ITrigger? trigger = interval switch
        {
            WsQuartzInterval.TimeSpan => WsQuartzUtils.CreateTrigger(triggerName, triggerGroup, timeSpan, repeatForever),
            WsQuartzInterval.Cron => WsQuartzUtils.CreateTrigger(triggerName, triggerGroup, cronExpression, repeatForever),
            _ => WsQuartzUtils.CreateTrigger(triggerName, triggerGroup, interval, length, repeatForever),
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

    private IJobDetail? CreateJobDetail(WsQuartzInterval interval, string cronExpression, Action action, string jobName)
    {
        IJobDetail? jobDetail = null;
        JobBuilder? jobBuilder = null;

        if (interval == WsQuartzInterval.Cron)
        {
            if (cronExpression == WsQuartzUtils.CronExpression.EverySeconds())
                interval = WsQuartzInterval.Seconds;
            else if (cronExpression == WsQuartzUtils.CronExpression.EverySeconds10())
                interval = WsQuartzInterval.Seconds10;
            else if (cronExpression == WsQuartzUtils.CronExpression.EveryMinutes())
                interval = WsQuartzInterval.Minutes;
            else if (cronExpression == WsQuartzUtils.CronExpression.EveryMinutes10())
                interval = WsQuartzInterval.Minutes10;
            else if (cronExpression == WsQuartzUtils.CronExpression.EveryHours())
                interval = WsQuartzInterval.Hours;
            else if (cronExpression == WsQuartzUtils.CronExpression.EveryDays())
                interval = WsQuartzInterval.Days;
        }

        switch (interval)
        {
            case WsQuartzInterval.Cron:
                WsQuartzJobEverySecondsModel.Actions.Add(action);
                jobBuilder = JobBuilder
                    .Create<WsQuartzJobModel>()
                    .WithIdentity(jobName);
                break;
            case WsQuartzInterval.Seconds:
                WsQuartzJobEverySecondsModel.Actions.Add(action);
                jobBuilder = JobBuilder
                    .Create<WsQuartzJobEverySecondsModel>()
                    .WithIdentity(jobName);
                break;
            case WsQuartzInterval.Seconds10:
                WsQuartzJobEverySeconds10Model.Actions.Add(action);
                jobBuilder = JobBuilder
                    .Create<WsQuartzJobEverySeconds10Model>()
                    .WithIdentity(jobName);
                break;
            case WsQuartzInterval.Minutes:
                WsQuartzJobEveryMinutesModel.Actions.Add(action);
                jobBuilder = JobBuilder
                    .Create<WsQuartzJobEveryMinutesModel>()
                    .WithIdentity(jobName);
                break;
            case WsQuartzInterval.Minutes10:
                WsQuartzJobEveryMinutes10Model.Actions.Add(action);
                jobBuilder = JobBuilder
                    .Create<WsQuartzJobEveryMinutes10Model>()
                    .WithIdentity(jobName);
                break;
            case WsQuartzInterval.Hours:
                WsQuartzJobEveryHoursModel.Actions.Add(action);
                jobBuilder = JobBuilder
                    .Create<WsQuartzJobEveryHoursModel>()
                    .WithIdentity(jobName);
                break;
            case WsQuartzInterval.Days:
                WsQuartzJobEveryDaysModel.Actions.Add(action);
                jobBuilder = JobBuilder
                    .Create<WsQuartzJobEveryDaysModel>()
                    .WithIdentity(jobName);
                break;
            case WsQuartzInterval.TimeSpan:
                break;
        }
        if (jobBuilder is not null)
            jobDetail = jobBuilder.Build();
        return jobDetail;
    }

    #endregion
}