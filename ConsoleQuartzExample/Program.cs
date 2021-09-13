// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.Schedulers;
using System;
using System.Threading.Tasks;

namespace ConsoleQuartzExample
{
    internal class Program
    {
        #region Public and private fields and properties

        private static readonly QuartzHelper _quartz = QuartzHelper.Instance;

        #endregion

        internal static async Task Main()
        {
            await Console.Out.WriteLineAsync("Start");
            await _quartz.OpenAsync();

            await Task.Delay(TimeSpan.FromMilliseconds(10)).ConfigureAwait(true);
            await Console.Out.WriteLineAsync("Finish");
            
            Console.ReadKey();
            await _quartz.CloseAsync();
        }
    }
}
