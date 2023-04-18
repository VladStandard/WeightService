// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.PlusBundlesFks;

namespace WsStorageCoreTests.TableScaleFkModels.PlusBundleFks;

[TestFixture]
public sealed class PluBundleFkModelTests
{
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        WsTestsUtils.DataCore.AssertSqlPropertyCheckDt<PluBundleFkModel>(nameof(WsSqlTableBase.CreateDt));
        WsTestsUtils.DataCore.AssertSqlPropertyCheckDt<PluBundleFkModel>(nameof(WsSqlTableBase.ChangeDt));
        WsTestsUtils.DataCore.AssertSqlPropertyCheckBool<PluBundleFkModel>(nameof(WsSqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        WsTestsUtils.DataCore.TableBaseModelAssertToString<PluBundleFkModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        WsTestsUtils.DataCore.TableBaseModelAssertEqualsNew<PluBundleFkModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        WsTestsUtils.DataCore.TableBaseModelAssertSerialize<PluBundleFkModel>();
    }
}