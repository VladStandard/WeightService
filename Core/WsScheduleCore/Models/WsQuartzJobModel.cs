namespace WsScheduleCore.Models;

public sealed class WsQuartzJobModel : IJob
{
    #region Public and private fields, properties, constructor

    private List<Action> Actions { get; } = new();

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