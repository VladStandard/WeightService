// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Templates;

namespace DataCoreTests.Sql.TableScaleModels.Templates;

[TestFixture]
internal class TemplateModelTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;
    
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCore.AssertSqlPropertyCheckDt<TemplateModel>(nameof(TemplateModel.CreateDt));
        DataCore.AssertSqlPropertyCheckDt<TemplateModel>(nameof(TemplateModel.ChangeDt));
        DataCore.AssertSqlPropertyCheckBool<TemplateModel>(nameof(SqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        DataCore.TableBaseModelAssertToString<TemplateModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCore.TableBaseModelAssertEqualsNew<TemplateModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        DataCore.TableBaseModelAssertSerialize<TemplateModel>();
    }
}