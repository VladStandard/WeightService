// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableDiagModels.LogsWebs;

namespace WsStorageCoreTests.TableDiagModels.LogsWebs;

[TestFixture]
internal class LogWebValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        LogWebModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<LogWebModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        LogWebModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<LogWebModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}