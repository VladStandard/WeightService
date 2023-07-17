using WsStorageCore.Views.ViewScaleModels.Devices;

namespace WsStorageCoreTests.Views.ViewScaleModels.Devices;

[TestFixture]
public sealed class ViewDevicesRepositoryTests
{
    private WsSqlViewDeviceRepository ViewDeviceRepository = WsSqlViewDeviceRepository.Instance;
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlCrudConfigModel sqlConfig = new WsSqlCrudConfigModel() { SelectTopRowsCount = 10 };
            List<WsSqlViewDeviceModel> items = ViewDeviceRepository.GetList(sqlConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}