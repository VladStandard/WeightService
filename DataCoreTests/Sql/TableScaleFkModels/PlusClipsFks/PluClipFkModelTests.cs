// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusClipsFks;

namespace DataCoreTests.Sql.TableScaleFkModels.PlusClipsFks;

[TestFixture]
internal class PluClipFkModelTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCore.AssertSqlPropertyCheckDt<PluClipFkModel>(nameof(SqlTableBase.CreateDt));
        DataCore.AssertSqlPropertyCheckDt<PluClipFkModel>(nameof(SqlTableBase.ChangeDt));
        DataCore.AssertSqlPropertyCheckBool<PluClipFkModel>(nameof(SqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        DataCore.TableBaseModelAssertToString<PluClipFkModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCore.TableBaseModelAssertEqualsNew<PluClipFkModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        DataCore.TableBaseModelAssertSerialize<PluClipFkModel>();
    }
}