// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.PlusScales;

namespace WsStorageCoreTests.TableScaleModels.PluScales;

[TestFixture]
public sealed class PluScaleModelTests
{
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        WsTestsUtils.DataCore.AssertSqlPropertyCheckDt<PluScaleModel>(nameof(WsSqlTableBase.CreateDt));
        WsTestsUtils.DataCore.AssertSqlPropertyCheckDt<PluScaleModel>(nameof(WsSqlTableBase.ChangeDt));
        WsTestsUtils.DataCore.AssertSqlPropertyCheckBool<PluScaleModel>(nameof(WsSqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        WsTestsUtils.DataCore.TableBaseModelAssertToString<PluScaleModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        WsTestsUtils.DataCore.TableBaseModelAssertEqualsNew<PluScaleModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        WsTestsUtils.DataCore.TableBaseModelAssertSerialize<PluScaleModel>();
    }
}