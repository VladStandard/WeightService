// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.DeviceTypes;

namespace WsStorageCoreTests.TableScaleModels.DeviceTypes;

[TestFixture]
public sealed class DeviceTypeValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        DeviceTypeModel item = WsTestsUtils.DataTests.CreateNewSubstitute<DeviceTypeModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        DeviceTypeModel item = WsTestsUtils.DataTests.CreateNewSubstitute<DeviceTypeModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}