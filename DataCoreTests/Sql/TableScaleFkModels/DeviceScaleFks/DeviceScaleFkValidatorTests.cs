// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;

namespace DataCoreTests.Sql.TableScaleFkModels.DeviceScaleFks;

[TestFixture]
internal class DeviceScaleFkValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        DeviceScaleFkModel item = DataCore.CreateNewSubstitute<DeviceScaleFkModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        DeviceScaleFkModel item = DataCore.CreateNewSubstitute<DeviceScaleFkModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}
