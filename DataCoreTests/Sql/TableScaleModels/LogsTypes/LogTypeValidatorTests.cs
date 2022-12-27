// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.LogsTypes;

namespace DataCoreTests.Sql.TableScaleModels.LogsTypes;

[TestFixture]
internal class LogTypeValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        LogTypeModel item = DataCore.CreateNewSubstitute<LogTypeModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        LogTypeModel item = DataCore.CreateNewSubstitute<LogTypeModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}
