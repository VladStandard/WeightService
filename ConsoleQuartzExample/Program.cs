// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.Schedulers;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ConsoleQuartzExample
{
    internal class Program
    {
        #region Public and private fields and properties

        private static readonly QuartzHelper _quartz = QuartzHelper.Instance;

        #endregion

        #region Public and private methods

        internal static void Method([CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            Console.Out.WriteLineAsync(
                $"{DateTime.Now}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}"
                ).ConfigureAwait(true);
        }

        internal static async Task Main()
        {
            await Console.Out.WriteLineAsync("Start");
            string cronExpression = QuartzUtils.CronExpression.EverySeconds(3);
            await Console.Out.WriteLineAsync($"{nameof(cronExpression)}: {cronExpression}");
            _quartz.AddJob(cronExpression, delegate { Method(); });
            await Task.Delay(TimeSpan.FromSeconds(10)).ConfigureAwait(true);
            await Console.Out.WriteLineAsync("Finish");
            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(true);
            await _quartz.CloseAsync();
        }

        #endregion
    }
}
