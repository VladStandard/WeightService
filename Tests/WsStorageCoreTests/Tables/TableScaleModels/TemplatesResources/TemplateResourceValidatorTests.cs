// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableScaleModels.TemplatesResources;

[TestFixture]
public sealed class TemplateResourceValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlTemplateResourceModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlTemplateResourceModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlTemplateResourceModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlTemplateResourceModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}