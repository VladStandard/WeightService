// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableDiagModels.Logs;

[TestFixture]
public sealed class LogValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        LogModel item = WsTestsUtils.DataTests.CreateNewSubstitute<LogModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        LogModel item = WsTestsUtils.DataTests.CreateNewSubstitute<LogModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}