using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableConfModels.DeviceSettingsFk;

[TestFixture]
public sealed class DeviceSettingsFkRepositoryTests : TableRepositoryTests
{
    private WsSqlDeviceSettingsFkRepository DeviceSettingsFkRepository = WsSqlDeviceSettingsFkRepository.Instance;
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlDeviceSettingsFkModel> items = DeviceSettingsFkRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}