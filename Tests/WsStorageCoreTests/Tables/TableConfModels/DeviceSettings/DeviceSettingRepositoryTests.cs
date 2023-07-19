using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableConfModels.DeviceSettings;

[TestFixture]
public sealed class DeviceSettingsRepositoryTests : TableRepositoryTests
{
    private WsSqlDeviceSettingsRepository DeviceSettingsRepository = WsSqlDeviceSettingsRepository.Instance;
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlDeviceSettingsModel> items = DeviceSettingsRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}