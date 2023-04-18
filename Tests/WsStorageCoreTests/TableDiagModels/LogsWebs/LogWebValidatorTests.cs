// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableDiagModels.LogsWebs;

namespace WsStorageCoreTests.TableDiagModels.LogsWebs;

[TestFixture]
public sealed class LogWebValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        LogWebModel item = WsTestsUtils.DataCore.CreateNewSubstitute<LogWebModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        LogWebModel item = WsTestsUtils.DataCore.CreateNewSubstitute<LogWebModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}