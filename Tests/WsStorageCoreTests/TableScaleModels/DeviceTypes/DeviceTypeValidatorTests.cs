// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.DeviceTypes;

[TestFixture]
public sealed class DeviceTypeValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlDeviceTypeModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlDeviceTypeModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlDeviceTypeModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlDeviceTypeModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}