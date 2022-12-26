// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Versions;

namespace DataCoreTests.Sql.TableScaleModels.Versions;

[TestFixture]
internal class VersionModelTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;
    
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCore.AssertSqlPropertyCheckDt<VersionModel>(nameof(VersionModel.CreateDt));
        DataCore.AssertSqlPropertyCheckDt<VersionModel>(nameof(VersionModel.ChangeDt));
        DataCore.AssertSqlPropertyCheckDt<VersionModel>(nameof(VersionModel.ReleaseDt));
        DataCore.AssertSqlPropertyCheckString<VersionModel>(nameof(VersionModel.Description));
        DataCore.AssertSqlPropertyCheckString<VersionModel>(nameof(VersionModel.Version));
        DataCore.AssertSqlPropertyCheckBool<VersionModel>(nameof(SqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        DataCore.TableBaseModelAssertToString<VersionModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCore.TableBaseModelAssertEqualsNew<VersionModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        DataCore.TableBaseModelAssertSerialize<VersionModel>();
    }
}