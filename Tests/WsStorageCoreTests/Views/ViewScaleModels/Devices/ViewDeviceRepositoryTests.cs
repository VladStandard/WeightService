using WsStorageCore.Views.ViewScaleModels.Devices;

namespace WsStorageCoreTests.Views.ViewScaleModels.Devices;

[TestFixture]
public sealed class ViewDevicesRepositoryTests : ViewRepositoryTests
{
    private IViewDeviceRepository ViewDeviceRepository { get; } = new WsSqlViewDeviceRepository();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewDeviceModel> items = ViewDeviceRepository.GetList(SqlCrudConfig);
            WsTestsUtils.DataTests.PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}