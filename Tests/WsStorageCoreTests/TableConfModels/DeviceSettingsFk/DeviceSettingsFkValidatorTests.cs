// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableConfModels.DeviceSettingsFk;

[TestFixture]
public sealed class DeviceSettingsFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlDeviceSettingsFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlDeviceSettingsFkModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlDeviceSettingsFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlDeviceSettingsFkModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}