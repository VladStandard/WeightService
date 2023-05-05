// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCoreTests.Helpers;

[TestFixture]
public sealed class WsUserSessionHelperTests
{
    private WsUserSessionHelper UserSession => WsUserSessionHelper.Instance;

    [Test]
    public void UserSession_DataContext_Greater()
    {
        WsTestsUtils.DataCore.AssertAction(() =>
        {
            List<WsSqlAppModel> apps = UserSession.ContextManager.ContextList.GetListNotNullableApps(new());
            Assert.Greater(apps.Count, 0);
        }, false);
    }
    
    [Test]
    public void UserSession_DataAccess_Greater()
    {
        WsTestsUtils.DataCore.AssertAction(() =>
        {
            WsSqlCrudConfigModel sqlCrudConfig = new(true, true, true, true, true);
            List<WsSqlDeviceModel> devices = UserSession.ContextManager.ContextList.GetListNotNullableDevices(sqlCrudConfig);
            Assert.Greater(devices.Count, 0);
        }, false);
    }
}