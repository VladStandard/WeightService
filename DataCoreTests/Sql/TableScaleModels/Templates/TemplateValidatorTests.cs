// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Templates;

namespace DataCoreTests.Sql.TableScaleModels.Templates;

[TestFixture]
internal class TemplateValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        TemplateModel item = DataCore.CreateNewSubstitute<TemplateModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        TemplateModel item = DataCore.CreateNewSubstitute<TemplateModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}
