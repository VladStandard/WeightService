// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Devices;

namespace DataCoreTests.Sql.TableScaleModels.Devices;

[TestFixture]
internal class DeviceValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        DeviceModel item = DataCore.CreateNewSubstitute<DeviceModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        DeviceModel item = DataCore.CreateNewSubstitute<DeviceModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}
