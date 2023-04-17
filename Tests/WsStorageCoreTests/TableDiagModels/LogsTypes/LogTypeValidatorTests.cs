// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.TableDiagModels.LogsTypes;

namespace WsStorageCoreTests.TableDiagModels.LogsTypes;

[TestFixture]
internal class LogTypeValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        LogTypeModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<LogTypeModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        LogTypeModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<LogTypeModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}