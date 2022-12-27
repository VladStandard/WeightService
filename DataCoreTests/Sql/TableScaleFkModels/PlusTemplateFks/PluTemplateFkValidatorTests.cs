// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusTemplatesFks;

namespace DataCoreTests.Sql.TableScaleFkModels.PlusTemplateFks;

[TestFixture]
internal class PluTemplateFkValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        PluTemplateFkModel item = DataCore.CreateNewSubstitute<PluTemplateFkModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluTemplateFkModel item = DataCore.CreateNewSubstitute<PluTemplateFkModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}
