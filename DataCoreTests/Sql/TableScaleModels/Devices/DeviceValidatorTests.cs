// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Devices;

namespace DataCoreTests.Sql.TableScaleModels.Devices;

[TestFixture]
internal class DeviceValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        DeviceModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<DeviceModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        DeviceModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<DeviceModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}