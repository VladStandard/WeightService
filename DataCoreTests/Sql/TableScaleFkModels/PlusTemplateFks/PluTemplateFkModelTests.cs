// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusTemplatesFks;

namespace DataCoreTests.Sql.TableScaleFkModels.PlusTemplateFks;

[TestFixture]
internal class PluTemplateFkModelTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCore.AssertSqlPropertyCheckDt<PluTemplateFkModel>(nameof(SqlTableBase.CreateDt));
        DataCore.AssertSqlPropertyCheckDt<PluTemplateFkModel>(nameof(SqlTableBase.ChangeDt));
        DataCore.AssertSqlPropertyCheckBool<PluTemplateFkModel>(nameof(SqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        DataCore.TableBaseModelAssertToString<PluTemplateFkModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCore.TableBaseModelAssertEqualsNew<PluTemplateFkModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        DataCore.TableBaseModelAssertSerialize<PluTemplateFkModel>();
    }
}