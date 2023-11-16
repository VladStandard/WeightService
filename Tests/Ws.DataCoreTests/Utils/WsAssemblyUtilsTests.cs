namespace Ws.DataCoreTests.Utils;

[TestFixture]
public sealed class WsAssemblyUtilsTests
{
    [Test]
    public void Get_app_version_is_work()
    {
        Assert.DoesNotThrow(() =>
        {
            string appVersion = AssemblyUtils.GetAppVersion(Assembly.GetExecutingAssembly());
            TestContext.WriteLine(appVersion);
            
            Assert.IsNotEmpty(appVersion);
        });
    }

    [Test]
    public void Get_run_is_work()
    {
        Assert.DoesNotThrow(() =>
        {
            string runDirectory = AssemblyUtils.GetRunDirectory();
            TestContext.WriteLine(runDirectory);
            
            Assert.IsNotEmpty(runDirectory);
        });
    }

    [Test]
    public void Get_lib_version_is_work()
    {
        Assert.DoesNotThrow(() =>
        {
            string libVersion = AssemblyUtils.GetLibVersion();
            TestContext.WriteLine(libVersion);

            Assert.IsNotEmpty(libVersion);
        });
    }
}