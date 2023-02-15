// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.TemplatesResources;

[TestFixture]
internal class TemplateResourceValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        TemplateResourceModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<TemplateResourceModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        TemplateResourceModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<TemplateResourceModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}