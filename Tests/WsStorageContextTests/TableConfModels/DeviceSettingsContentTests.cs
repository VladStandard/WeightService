// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageContextTests.TableConfModels;

[TestFixture]
public sealed class DeviceSettingsContentTests
{
    [Test]
    public void Validate_device_settings()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlDeviceSettingsModel>(WsSqlEnumIsMarked.ShowAll);
    }

    [Test]
    public void Get_table_devices_settings_show_all()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlDeviceSettingsModel> items = WsTestsUtils.DataTests.ContextManager.DeviceSettingsRepository.GetList();
            Assert.IsTrue(items.Any());
            WsTestsUtils.DataTests.PrintTopRecords(items, 0);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}