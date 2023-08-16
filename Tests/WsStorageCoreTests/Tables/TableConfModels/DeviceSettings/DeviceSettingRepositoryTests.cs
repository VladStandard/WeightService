namespace WsStorageCoreTests.Tables.TableConfModels.DeviceSettings;

[TestFixture]
public sealed class DeviceSettingsRepositoryTests : TableRepositoryTests
{
    private WsSqlDeviceSettingsRepository DeviceSettingsRepository { get; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlDeviceSettingsModel> items = DeviceSettingsRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}