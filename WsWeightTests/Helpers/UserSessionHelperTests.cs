// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using DataCore.Sql.TableScaleModels.Apps;

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
            List<AppModel> apps = UserSession.DataContext.GetListNotNullable<AppModel>(new());
            Assert.Greater(apps.Count, 0);
        }, false);
    }
}