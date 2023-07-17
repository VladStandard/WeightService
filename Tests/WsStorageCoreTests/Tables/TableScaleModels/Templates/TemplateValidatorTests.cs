// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableScaleModels.Templates;

[TestFixture]
public sealed class TemplateValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlTemplateModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlTemplateModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlTemplateModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlTemplateModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}