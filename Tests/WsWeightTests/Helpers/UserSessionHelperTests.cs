// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using WsDataCore.Sql.Models;
using WsDataCore.Sql.TableScaleModels.Apps;
using WsDataCore.Sql.TableScaleModels.Devices;
using WsWeightCore.Helpers;

namespace WsWeightTests.Helpers;

[TestFixture]
public class UserSessionHelperTests
{
    private UserSessionHelper UserSession => UserSessionHelper.Instance;

    [Test]
    public void UserSession_DataContext_Greater()
    {
        DataCoreTestsUtils.DataCore.AssertAction(() =>
        {
            List<AppModel> apps = UserSession.DataContext.GetListNotNullableApps(new());
            Assert.Greater(apps.Count, 0);
        }, false);
    }
    
    [Test]
    public void UserSession_DataAccess_Greater()
    {
        DataCoreTestsUtils.DataCore.AssertAction(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = new(true, true, true, true, true);
            List<DeviceModel> devices = UserSession.DataContext.GetListNotNullableDevices(sqlCrudConfig);
            Assert.Greater(devices.Count, 0);
        }, false);
    }
}