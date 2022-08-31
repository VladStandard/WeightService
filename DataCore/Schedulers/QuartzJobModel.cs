// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Quartz;
using System.Threading.Tasks;

namespace DataCore.Schedulers;

public class QuartzJobModel : IJob
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
            action.Invoke();
        }
    }

    #endregion
}
