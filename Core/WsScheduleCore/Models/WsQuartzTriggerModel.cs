// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsScheduleCore.Enums;

namespace WsScheduleCore.Models;

public class WsQuartzTriggerModel
{
    #region Public and private fields, properties, constructor

    public WsQuartzInterval Interval { get; } = WsQuartzInterval.Seconds;
    public int IntervalLength { get; set; }
    public TimeSpan TimeSpanValue { get; set; } = new(0);
    public string CronExpression { get; set; } = string.Empty;
    public bool RepeatForever { get; }

    public string Name { get; }
    public string Group { get; }

    public ITrigger? _trigger;
    public ITrigger? Trigger
    {
        get
        {
            // Checks.
            switch (Interval)
            {
                case WsQuartzInterval.TimeSpan:
                    if (TimeSpanValue == null || TimeSpanValue.Ticks <= 0)
                    {
                        _trigger = null;
                        throw new ArgumentException($"Use {nameof(TimeSpanValue)} with value more then 0!");
                    }
                    break;
                case WsQuartzInterval.Cron:
                    if (string.IsNullOrEmpty(CronExpression))
                    {
                        _trigger = null;
                        throw new ArgumentException($"Use {nameof(CronExpression)} with correct value!");
                    }
                    break;
                default:
                    if (IntervalLength <= 0)
                    {
                        _trigger = null;
                        throw new ArgumentException($"Use {nameof(IntervalLength)} with value more then 0!");
                    }
                    break;
            }

            if (_trigger == null)
            {
                TriggerBuilder? triggerBuilder = TriggerBuilder.Create()
                    //.WithIdentity($"{nameof(_trigger)}_{IntervalLength}")
                    .WithIdentity(Name, Group)
                    ;
                switch (Interval)
                {
                    case WsQuartzInterval.TimeSpan:
                        if (RepeatForever)
                            _ = triggerBuilder.WithSimpleSchedule(x => x.WithInterval(TimeSpanValue).RepeatForever());
                        else
                            _ = triggerBuilder.WithSimpleSchedule(x => x.WithInterval(TimeSpanValue));
                        break;
                    case WsQuartzInterval.Seconds:
                        if (RepeatForever)
                            _ = triggerBuilder.WithSimpleSchedule(x => x.WithIntervalInSeconds(IntervalLength).RepeatForever());
                        else
                            _ = triggerBuilder.WithSimpleSchedule(x => x.WithIntervalInSeconds(IntervalLength));
                        break;
                    case WsQuartzInterval.Minutes:
                        if (RepeatForever)
                            _ = triggerBuilder.WithSimpleSchedule(x => x.WithIntervalInMinutes(IntervalLength).RepeatForever());
                        else
                            _ = triggerBuilder.WithSimpleSchedule(x => x.WithIntervalInMinutes(IntervalLength));
                        break;
                    case WsQuartzInterval.Hours:
                        if (RepeatForever)
                            _ = triggerBuilder.WithSimpleSchedule(x => x.WithIntervalInHours(IntervalLength).RepeatForever());
                        else
                            _ = triggerBuilder.WithSimpleSchedule(x => x.WithIntervalInHours(IntervalLength));
                        break;
                    case WsQuartzInterval.Cron:
                        _ = triggerBuilder
                            .WithCronSchedule(CronExpression, x => x
                                .WithMisfireHandlingInstructionFireAndProceed()
                            );
                        break;
                    case WsQuartzInterval.Seconds10:
                        break;
                    case WsQuartzInterval.Days:
                        break;
                }
                //triggerBuilder.StartNow();
                _trigger = triggerBuilder.Build();
            }
            return _trigger;
        }
    }

    #endregion

    #region Constructor and destructor

    private WsQuartzTriggerModel(string name, string group, WsQuartzInterval interval, bool repeatForever)
    {
        Interval = interval;
        RepeatForever = repeatForever;
        Name = name;
        Group = group;
    }

    public WsQuartzTriggerModel(string name, string group, WsQuartzInterval interval, int length, bool repeatForever) : this(name, group, interval, repeatForever)
        => IntervalLength = length;

    public WsQuartzTriggerModel(string name, string group, TimeSpan timeSpan, bool repeatForever) : this(name, group, WsQuartzInterval.TimeSpan, repeatForever)
        => TimeSpanValue = timeSpan;

    public WsQuartzTriggerModel(string name, string group, string cronExpression, bool repeatForever) : this(name, group, WsQuartzInterval.Cron, repeatForever)
        => CronExpression = cronExpression;

    #endregion
}