// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusTemplateFks;

[TestFixture]
public sealed class PluTemplateFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlPluTemplateFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluTemplateFkModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlPluTemplateFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluTemplateFkModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}