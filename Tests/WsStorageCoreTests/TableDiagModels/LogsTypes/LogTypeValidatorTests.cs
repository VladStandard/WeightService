// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableDiagModels.LogsTypes;

namespace WsStorageCoreTests.TableDiagModels.LogsTypes;

[TestFixture]
public sealed class LogTypeValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        LogTypeModel item = WsTestsUtils.DataCore.CreateNewSubstitute<LogTypeModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        LogTypeModel item = WsTestsUtils.DataCore.CreateNewSubstitute<LogTypeModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}