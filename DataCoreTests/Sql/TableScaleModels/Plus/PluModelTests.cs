// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Plus;

namespace DataCoreTests.Sql.TableScaleModels.Plus;

[TestFixture]
internal class PluModelTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;
   
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCore.AssertSqlPropertyCheckDt<PluModel>(nameof(PluModel.CreateDt));
        DataCore.AssertSqlPropertyCheckDt<PluModel>(nameof(PluModel.ChangeDt));
        DataCore.AssertSqlPropertyCheckBool<PluModel>(nameof(SqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        DataCore.TableBaseModelAssertToString<PluModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCore.TableBaseModelAssertEqualsNew<PluModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        DataCore.TableBaseModelAssertSerialize<PluModel>();
    }
}