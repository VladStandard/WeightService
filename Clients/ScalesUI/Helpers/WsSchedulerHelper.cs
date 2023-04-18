// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesUI.Helpers;

#nullable enable
internal class WsSchedulerHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSchedulerHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSchedulerHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private UserSessionHelper UserSession => UserSessionHelper.Instance;
    private WsQuartzHelper Quartz => WsQuartzHelper.Instance;
    private readonly object _lockerMinutes10 = new();
    private readonly object _lockerHours = new();
    private readonly object _lockerDays = new();
    private Form? FormMain { get; set; }

    #endregion

    #region Public and private methods

    public void Close() => Quartz.Close();

    public void Load(Form form)
    {
        FormMain = form;

        Quartz.AddJob(WsQuartzUtils.CronExpression.EveryMinutes10(), ScheduleEveryMinutes10,
            $"job{nameof(ScheduleEveryMinutes10)}", $"trigger{nameof(ScheduleEveryMinutes10)}",
            $"triggerGroup{nameof(ScheduleEveryHours)}");
        Quartz.AddJob(WsQuartzUtils.CronExpression.EveryHours(), ScheduleEveryHours,
            $"job{nameof(ScheduleEveryHours)}", $"trigger{nameof(ScheduleEveryHours)}",
            $"triggerGroup{nameof(ScheduleEveryHours)}");
        Quartz.AddJob(WsQuartzUtils.CronExpression.EveryDays(), ScheduleEveryDays,
            $"job{nameof(ScheduleEveryDays)}", $"trigger{nameof(ScheduleEveryDays)}",
            $"triggerGroup{nameof(ScheduleEveryDays)}");
    }

    private void ScheduleEveryMinutes10()
    {
        if (FormMain is null) throw new ArgumentException(nameof(FormMain));

        lock (_lockerMinutes10)
        {
            ActionUtils.ActionTryCatch(FormMain, UserSession.Scale, () =>
            {
                UserSession.DataContext.DataAccess.SaveLogMemory(
                    UserSession.PluginMemory.GetMemorySizeAppMb(), UserSession.PluginMemory.GetMemorySizeFreeMb());
                GC.Collect();
            }
            );
        }
    }

    private void ScheduleEveryHours()
    {
        if (FormMain is null) throw new ArgumentException(nameof(FormMain));

        lock (_lockerHours)
        {
            ActionUtils.ActionTryCatch(FormMain, UserSession.Scale, 
                () => ActionUtils.ActionMakeScreenShot(FormMain, UserSession.Scale));
        }
    }

    private void ScheduleEveryDays()
    {
        if (FormMain is null) throw new ArgumentException(nameof(FormMain));

        lock (_lockerDays)
        {
            UserSession.ProductDate = DateTime.Now;
            ActionUtils.ActionMakeScreenShot(FormMain, UserSession.Scale);
        }
    }

    #endregion
}