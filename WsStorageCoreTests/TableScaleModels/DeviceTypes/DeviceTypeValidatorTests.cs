// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.DeviceTypes;

[TestFixture]
internal class DeviceTypeValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        DeviceTypeModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<DeviceTypeModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        DeviceTypeModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<DeviceTypeModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}