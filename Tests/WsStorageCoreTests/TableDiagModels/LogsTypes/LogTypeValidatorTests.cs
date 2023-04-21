// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableDiagModels.LogsTypes;

[TestFixture]
public sealed class LogTypeValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        LogTypeModel item = WsTestsUtils.DataTests.CreateNewSubstitute<LogTypeModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        LogTypeModel item = WsTestsUtils.DataTests.CreateNewSubstitute<LogTypeModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}