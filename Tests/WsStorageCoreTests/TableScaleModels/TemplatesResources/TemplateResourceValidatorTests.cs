// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.TemplatesResources;

namespace WsStorageCoreTests.TableScaleModels.TemplatesResources;

[TestFixture]
public sealed class TemplateResourceValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        TemplateResourceModel item = WsTestsUtils.DataTests.CreateNewSubstitute<TemplateResourceModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        TemplateResourceModel item = WsTestsUtils.DataTests.CreateNewSubstitute<TemplateResourceModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}