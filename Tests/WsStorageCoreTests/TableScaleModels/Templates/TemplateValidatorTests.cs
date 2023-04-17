// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.TableScaleModels.Templates;

namespace WsStorageCoreTests.TableScaleModels.Templates;

[TestFixture]
internal class TemplateValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        TemplateModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<TemplateModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        TemplateModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<TemplateModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}