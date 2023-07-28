using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableConfModels.DeviceSettingsFk;

[TestFixture]
public sealed class DeviceSettingsFkRepositoryTests : TableRepositoryTests
{
    private WsSqlDeviceSettingsFkRepository DeviceSettingsFkRepository { get; } = new();
    private WsSqlDeviceRepository DeviceRepository { get; } = new();
    private WsSqlDeviceSettingsRepository DeviceSettingsRepository { get; } = new();

    [Test, Order(1)]
    public void FillSettings()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlDeviceModel> devices = DeviceRepository.GetList(SqlCrudConfig);
            List<WsSqlDeviceSettingsModel> deviceSettings = DeviceSettingsRepository.GetList(SqlCrudConfig);
            List<WsSqlDeviceSettingsFkModel> deviceSettingsFks = DeviceSettingsFkRepository.GetList(SqlCrudConfig);

            foreach (WsSqlDeviceModel device in devices)
            foreach (WsSqlDeviceSettingsModel setting in deviceSettings)
            {
                WsSqlDeviceSettingsFkModel? deviceSettingsFk = deviceSettingsFks.Find(
                    x => x.Device.Equals(device) && x.Setting.Equals(setting));
                if (deviceSettingsFk is not null)
                    continue;
                deviceSettingsFk = DeviceSettingsFkRepository.GetNewItem();
                deviceSettingsFk.UpdateProperties(deviceSettingsFk, device, setting);
                DeviceSettingsFkRepository.SaveItemAsync(deviceSettingsFk);
            }

            TestContext.WriteLine("All settings added");
        }, false, DefaultPublishTypes);
    }

    [Test, Order(2)]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlDeviceSettingsFkModel> items = DeviceSettingsFkRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
}