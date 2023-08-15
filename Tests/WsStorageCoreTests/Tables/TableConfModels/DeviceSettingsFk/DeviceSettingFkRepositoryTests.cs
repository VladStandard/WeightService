namespace WsStorageCoreTests.Tables.TableConfModels.DeviceSettingsFk;

[TestFixture]
public sealed class DeviceSettingsFkRepositoryTests : TableRepositoryTests
{
    private WsSqlDeviceSettingsFkRepository DeviceSettingsFkRepository { get; } = new();
    private WsSqlDeviceRepository DeviceRepository { get; } = new();
    private WsSqlDeviceSettingsRepository DeviceSettingsRepository { get; } = new();

    [Test, Order(1)]
    public void Fill_settings()
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

            TestContext.WriteLine(WsLocaleCore.Tests.AllSettingsForAllDevicesWasAdded);
        }, false, DefaultConfigurations);
    }

    [Test, Order(2)]
    public void Get_list()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlDeviceSettingsFkModel> list = DeviceSettingsFkRepository.GetList(SqlCrudConfig);
            ParseRecords(list);
        }, false, DefaultConfigurations);
    }

    [Test, Order(3)]
    public void Get_list_by_device()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlDeviceModel> devices = DeviceRepository.GetList(SqlCrudConfig);
            ParseRecords(devices);

            foreach (WsSqlDeviceModel device in devices)
            {
                List<WsSqlDeviceSettingsFkModel> deviceSettings = DeviceSettingsFkRepository.GetListByDevice(device);
                ParseRecords(deviceSettings);
            }
        }, false, DefaultConfigurations);
    }
}