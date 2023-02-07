// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Runtime.CompilerServices;
using WsSchedule.Helpers;
using WsSchedule.Utils;

namespace DataCoreTests.Schedulers;

[TestFixture]
internal class QuartzHelperTests
{
    #region Public and private fields and properties

    private static QuartzHelper Quartz { get; } = QuartzHelper.Instance;

    #endregion

    private static void Method()
    {
        TestContext.WriteLine($"{DateTime.Now}");
    }

    [Test]
    public void OpenClose_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            TestContext.WriteLine("Open");
            Quartz.AddJob(QuartzUtils.CronExpression.EverySeconds(), Method, "jobName", "triggerName", "triggerGroup");

            Quartz.Close();
            TestContext.WriteLine("Close");
        });
        TestContext.WriteLine();
    }

    //[Test]
    //public void OpenClose_Throw()
    //{
    //    TestsUtils.MethodStart();

    //    Assert.Throws<ArgumentException>(() =>
    //    {
    //        TestContext.WriteLine("Open");
    //        Quartz.AddJob(QuartzUtils.CronExpression.EverySeconds(), Method, "jobName", "triggerName", "triggerGroup");

    //        TestContext.WriteLine("Close");
    //        Quartz.Close();
    //    });
    //    TestContext.WriteLine();

    //    TestsUtils.MethodComplete();
    //}
}
