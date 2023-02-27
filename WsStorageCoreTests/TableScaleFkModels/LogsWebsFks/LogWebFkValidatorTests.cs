// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleFkModels.LogsWebsFks;

[TestFixture]
internal class LogWebFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        LogWebFkModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<LogWebFkModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        LogWebFkModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<LogWebFkModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}