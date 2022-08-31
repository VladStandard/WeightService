// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Quartz;

namespace DataCore.Schedulers;

public class QuartzTriggerModel
{
    #region Public and private fields, properties, constructor

    public QuartzEnums.Interval Interval { get; } = QuartzEnums.Interval.Seconds;
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
                case QuartzEnums.Interval.TimeSpan:
                    if (TimeSpanValue == null || TimeSpanValue.Ticks <= 0)
                    {
                        _trigger = null;
                        throw new ArgumentException($"Use {nameof(TimeSpanValue)} with value more then 0!");
                    }
                    break;
                case QuartzEnums.Interval.Cron:
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
                    case QuartzEnums.Interval.TimeSpan:
                        if (RepeatForever)
                            _ = triggerBuilder.WithSimpleSchedule(x => x.WithInterval(TimeSpanValue).RepeatForever());
                        else
                            _ = triggerBuilder.WithSimpleSchedule(x => x.WithInterval(TimeSpanValue));
                        break;
                    case QuartzEnums.Interval.Seconds:
                        if (RepeatForever)
                            _ = triggerBuilder.WithSimpleSchedule(x => x.WithIntervalInSeconds(IntervalLength).RepeatForever());
                        else
                            _ = triggerBuilder.WithSimpleSchedule(x => x.WithIntervalInSeconds(IntervalLength));
                        break;
                    case QuartzEnums.Interval.Minutes:
                        if (RepeatForever)
                            _ = triggerBuilder.WithSimpleSchedule(x => x.WithIntervalInMinutes(IntervalLength).RepeatForever());
                        else
                            _ = triggerBuilder.WithSimpleSchedule(x => x.WithIntervalInMinutes(IntervalLength));
                        break;
                    case QuartzEnums.Interval.Hours:
                        if (RepeatForever)
                            _ = triggerBuilder.WithSimpleSchedule(x => x.WithIntervalInHours(IntervalLength).RepeatForever());
                        else
                            _ = triggerBuilder.WithSimpleSchedule(x => x.WithIntervalInHours(IntervalLength));
                        break;
                    case QuartzEnums.Interval.Cron:
                        _ = triggerBuilder
                            .WithCronSchedule(CronExpression, x => x
                                .WithMisfireHandlingInstructionFireAndProceed()
                            );
                        break;
                    case QuartzEnums.Interval.Seconds10:
                        break;
                    case QuartzEnums.Interval.Days:
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

    private QuartzTriggerModel(string name, string group, QuartzEnums.Interval interval, bool repeatForever)
    {
        Interval = interval;
        RepeatForever = repeatForever;
        Name = name;
        Group = group;
    }

    public QuartzTriggerModel(string name, string group, QuartzEnums.Interval interval, int length, bool repeatForever) : this(name, group, interval, repeatForever)
        => IntervalLength = length;

    public QuartzTriggerModel(string name, string group, TimeSpan timeSpan, bool repeatForever) : this(name, group, QuartzEnums.Interval.TimeSpan, repeatForever)
        => TimeSpanValue = timeSpan;

    public QuartzTriggerModel(string name, string group, string cronExpression, bool repeatForever) : this(name, group, QuartzEnums.Interval.Cron, repeatForever)
        => CronExpression = cronExpression;

    #endregion
}
