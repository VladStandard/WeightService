// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Deployment;

namespace WsDataCoreTests.Utils;

[TestFixture]
public sealed class WsAssemblyUtilsTests
{
    #region Public methods

    [Test]
    public void Get_app_version_is_work()
    {
        Assert.DoesNotThrow(() =>
        {
            string appVersion = WsAssemblyUtils.GetAppVersion(Assembly.GetExecutingAssembly());
            TestContext.WriteLine(appVersion);

            Assert.IsNotEmpty(appVersion);
        });
    }

    [Test]
    public void Get_click_once_is_work()
    {
        Assert.DoesNotThrow(() =>
        {
            string clickOnceDirectory = WsAssemblyUtils.GetClickOnceNetworkInstallDirectory();
            TestContext.WriteLine(clickOnceDirectory);

            Assert.IsNotEmpty(clickOnceDirectory);
        });
    }

    [Test]
    public void Get_run_is_work()
    {
        Assert.DoesNotThrow(() =>
        {
            string runDirectory = WsAssemblyUtils.GetRunDirectory();
            TestContext.WriteLine(runDirectory);

            Assert.IsNotEmpty(runDirectory);
        });
    }

    [Test]
    public void Get_lib_version_is_work()
    {
        Assert.DoesNotThrow(() =>
        {
            string libVersion = WsAssemblyUtils.GetLibVersion();
            TestContext.WriteLine(libVersion);

            Assert.IsNotEmpty(libVersion);
        });
    }

    #endregion
}