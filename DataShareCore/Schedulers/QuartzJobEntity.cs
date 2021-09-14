// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Quartz;
using System;
using System.Threading.Tasks;

namespace DataShareCore.Schedulers
{
    public class QuartzJobEntity : IJob
    {
        #region Public and private fields and properties

        public static Action? ActionMethod { get; set; } = null;

        #endregion

        #region Public and private methods

        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            if (ActionMethod != null)
            {
                ActionMethod.Invoke();
            }
            else
            {
                await Console.Out.WriteLineAsync($"{DateTime.Now}. Empty {nameof(ActionMethod)}!").ConfigureAwait(false);
            }
        }

        #endregion
    }
}
