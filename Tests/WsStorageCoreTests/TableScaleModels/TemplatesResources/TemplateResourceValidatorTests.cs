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
        TemplateResourceModel item = WsTestsUtils.DataCore.CreateNewSubstitute<TemplateResourceModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        TemplateResourceModel item = WsTestsUtils.DataCore.CreateNewSubstitute<TemplateResourceModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}