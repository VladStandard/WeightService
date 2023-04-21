// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.Templates;

[TestFixture]
public sealed class TemplateValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        TemplateModel item = WsTestsUtils.DataTests.CreateNewSubstitute<TemplateModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        TemplateModel item = WsTestsUtils.DataTests.CreateNewSubstitute<TemplateModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}