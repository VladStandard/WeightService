// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataShareCore.Schedulers
{
    public class QuartzJobEntity : IJob
    {
        #region Public and private fields and properties

        public static Dictionary<Action, string> Actions { get; set; } = new Dictionary<Action, string>();
        //public Action? ActionMethod { get; set; } = null;

        #endregion

        #region Public and private methods

        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            foreach (KeyValuePair<Action, string> action in Actions)
            {
                // ActionMethod?.Invoke();
                action.Key?.Invoke();
            }
        }

        #endregion
    }
}
