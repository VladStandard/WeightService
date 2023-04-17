// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.Tables;
using WsStorage.TableScaleModels.Versions;

namespace WsStorageCoreTests.TableScaleModels.Versions;

[TestFixture]
internal class VersionModelTests
{
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCoreTestsUtils.DataCore.AssertSqlPropertyCheckDt<VersionModel>(nameof(WsSqlTableBase.CreateDt));
        DataCoreTestsUtils.DataCore.AssertSqlPropertyCheckDt<VersionModel>(nameof(WsSqlTableBase.ChangeDt));
        DataCoreTestsUtils.DataCore.AssertSqlPropertyCheckDt<VersionModel>(nameof(VersionModel.ReleaseDt));
        DataCoreTestsUtils.DataCore.AssertSqlPropertyCheckString<VersionModel>(nameof(WsSqlTableBase.Description));
        DataCoreTestsUtils.DataCore.AssertSqlPropertyCheckString<VersionModel>(nameof(VersionModel.Version));
        DataCoreTestsUtils.DataCore.AssertSqlPropertyCheckBool<VersionModel>(nameof(WsSqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        DataCoreTestsUtils.DataCore.TableBaseModelAssertToString<VersionModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCoreTestsUtils.DataCore.TableBaseModelAssertEqualsNew<VersionModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        DataCoreTestsUtils.DataCore.TableBaseModelAssertSerialize<VersionModel>();
    }
}