// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Packages;

namespace DataCoreTests.Sql.TableScaleModels.Packages;

[TestFixture]
internal class PackageModelTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;
   
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCore.AssertSqlPropertyCheckDt<PackageModel>(nameof(PackageModel.CreateDt));
        DataCore.AssertSqlPropertyCheckDt<PackageModel>(nameof(PackageModel.ChangeDt));
        DataCore.AssertSqlPropertyCheckBool<PackageModel>(nameof(SqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        DataCore.TableBaseModelAssertToString<PackageModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCore.TableBaseModelAssertEqualsNew<PackageModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        DataCore.TableBaseModelAssertSerialize<PackageModel>();
    }
}