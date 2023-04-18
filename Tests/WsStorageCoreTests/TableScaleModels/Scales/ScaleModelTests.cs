// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Scales;

namespace WsStorageCoreTests.TableScaleModels.Scales;

[TestFixture]
public sealed class ScaleModelTests
{
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        WsTestsUtils.DataCore.AssertSqlPropertyCheckDt<ScaleModel>(nameof(WsSqlTableBase.CreateDt));
        WsTestsUtils.DataCore.AssertSqlPropertyCheckDt<ScaleModel>(nameof(WsSqlTableBase.ChangeDt));
        WsTestsUtils.DataCore.AssertSqlPropertyCheckBool<ScaleModel>(nameof(WsSqlTableBase.IsMarked));
        //WsTestsUtils.DataCore.AssertSqlFieldStringCheck<ScaleModel>(nameof(ScaleModel.Host.Name));
        WsTestsUtils.DataCore.AssertSqlPropertyCheckString<ScaleModel>(nameof(WsSqlTableBase.Description));
    }

    [Test]
    public void Model_ToString()
    {
        WsTestsUtils.DataCore.TableBaseModelAssertToString<ScaleModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        WsTestsUtils.DataCore.TableBaseModelAssertEqualsNew<ScaleModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        WsTestsUtils.DataCore.TableBaseModelAssertSerialize<ScaleModel>();
    }
}