using WsStorageCore.Views.ViewScaleModels.Devices;

namespace WsStorageCoreTests.Views.ViewScaleModels.Devices;

[TestFixture]
public sealed class ViewDevicesRepositoryTests : ViewRepositoryTests
{
    private IViewDeviceRepository ViewDeviceRepository = WsSqlViewDeviceRepository.Instance;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewDeviceModel> items = ViewDeviceRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}