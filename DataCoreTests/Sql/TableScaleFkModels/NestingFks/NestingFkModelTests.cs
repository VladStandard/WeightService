// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.NestingFks;

namespace DataCoreTests.Sql.TableScaleFkModels.NestingFks;

[TestFixture]
internal class NestingFkModelTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;
    
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCore.AssertSqlPropertyCheckDt<NestingFkModel>(nameof(SqlTableBase.CreateDt));
        DataCore.AssertSqlPropertyCheckDt<NestingFkModel>(nameof(SqlTableBase.ChangeDt));
        DataCore.AssertSqlPropertyCheckBool<NestingFkModel>(nameof(SqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        DataCore.TableBaseModelAssertToString<NestingFkModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCore.TableBaseModelAssertEqualsNew<NestingFkModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        DataCore.TableBaseModelAssertSerialize<NestingFkModel>();
    }
}


