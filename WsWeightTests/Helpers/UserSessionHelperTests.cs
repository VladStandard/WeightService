// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using DataCore.Sql.TableScaleModels.Apps;
using DataCore.Sql.TableScaleModels.Devices;

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
            List<DeviceModel> devices = UserSession.DataAccess.GetListDevices(true, true, true);
            Assert.Greater(devices.Count, 0);
        }, false);
    }
}