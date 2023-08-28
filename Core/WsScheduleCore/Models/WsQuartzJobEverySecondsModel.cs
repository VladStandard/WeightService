namespace WsScheduleCore.Models;

public sealed class WsQuartzJobEverySecondsModel : IJob
{
    #region Public and private fields, properties, constructor

    public static List<Action> Actions { get; } = new();

    #endregion

    #region Public and private methods

    public async Task Execute(IJobExecutionContext context)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        foreach (Action action in Actions)
        {
            action();
        }
    }

    #endregion
}