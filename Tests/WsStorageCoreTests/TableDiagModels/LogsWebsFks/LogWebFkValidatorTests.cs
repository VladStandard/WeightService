// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableDiagModels.LogsWebsFks;

[TestFixture]
public sealed class LogWebFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        LogWebFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<LogWebFkModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        LogWebFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<LogWebFkModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}