namespace WsScheduleCoreTests.Helpers;

[TestFixture]
public sealed class WsQuartzHelperTests
{
    #region Public and private fields and properties

    private static WsQuartzHelper Quartz { get; } = WsQuartzHelper.Instance;

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
            Quartz.AddJob(WsQuartzUtils.CronExpression.EverySeconds(), Method, "jobName", "triggerName", "triggerGroup");

            Quartz.Close();
            TestContext.WriteLine("Close");
        });
        TestContext.WriteLine();
    }
}
