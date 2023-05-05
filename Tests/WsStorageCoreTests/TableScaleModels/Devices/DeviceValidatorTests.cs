// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.Devices;

[TestFixture]
public sealed class DeviceValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlDeviceModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlDeviceModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlDeviceModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlDeviceModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}