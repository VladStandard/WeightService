// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Logs;

namespace DataCoreTests.Sql.TableScaleModels.Logs;

[TestFixture]
internal class LogValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        LogModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<LogModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        LogModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<LogModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}