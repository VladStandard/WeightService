// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.Schedulers;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ConsoleQuartzExample
{
    internal class Program
    {
        #region Public and private fields and properties

        private static QuartzEntity Quartz { get; set; } = QuartzEntity.Instance;

        #endregion

        #region Public and private methods

        internal static void Method1()
        {
            Console.Out.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Method1");
        }

        internal static void Method2()
        {
            Console.Out.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Method2 --");
        }

        internal static void Method3()
        {
            Console.Out.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Method3 ===");
        }

        internal static async Task Main()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            await Console.Out.WriteLineAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Start schedule. Wait 10 minutes. Press enter to exit.");
            Quartz.AddJob(QuartzUtils.CronExpression.EverySeconds(), delegate { Method1(); }, "jobSample1", "triggerName1", "triggerGroup1");
            Quartz.AddJob(QuartzUtils.CronExpression.EverySeconds(10), delegate { Method2(); }, "jobSample2", "triggerName2", "triggerGroup2");
            Quartz.AddJob(QuartzUtils.CronExpression.EverySeconds(15), delegate { Method1(); }, "jobSample3", "triggerName3", "triggerGroup3");
            while (true)
            {
                if (stopwatch.Elapsed.Minutes > 9)
                    break;
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                    break;
                System.Threading.Thread.Sleep(10);
            }
            Quartz.Close();
            await Console.Out.WriteLineAsync("Finish");
        }

        #endregion
    }
}
