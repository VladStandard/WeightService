// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableConfModels.DeviceSettings;

[TestFixture]
public sealed class DeviceSettingsValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlDeviceSettingsModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlDeviceSettingsModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlDeviceSettingsModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlDeviceSettingsModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}