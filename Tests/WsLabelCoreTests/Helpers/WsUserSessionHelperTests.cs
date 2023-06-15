// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Common;

namespace WsLabelCoreTests.Helpers;

[TestFixture]
public sealed class WsUserSessionHelperTests
{
    private WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;

    [Test]
    public void UserSession_DataContext_Greater()
    {
        WsTestsUtils.DataCore.AssertAction(() =>
        {
            List<WsSqlAppModel> apps = ContextManager.ContextList.GetListNotNullableApps(new());
            Assert.Greater(apps.Count, 0);
        }, false);
    }
    
    [Test]
    public void UserSession_DataAccess_Greater()
    {
        WsTestsUtils.DataCore.AssertAction(() =>
        {
            WsSqlCrudConfigModel sqlCrudConfig = new(WsSqlEnumIsMarked.ShowAll, true, true, true, true);
            List<WsSqlDeviceModel> devices = ContextManager.ContextList.GetListNotNullableDevices(sqlCrudConfig);
            Assert.Greater(devices.Count, 0);
        }, false);
    }
}