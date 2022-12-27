// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Logs;

namespace DataCoreTests.Sql.TableScaleModels.Logs;

[TestFixture]
internal class LogValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        LogModel item = DataCore.CreateNewSubstitute<LogModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        LogModel item = DataCore.CreateNewSubstitute<LogModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}
