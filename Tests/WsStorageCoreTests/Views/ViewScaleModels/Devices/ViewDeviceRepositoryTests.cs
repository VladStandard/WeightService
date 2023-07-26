using NUnit.Framework.Constraints;
using WsStorageCore.Views.ViewScaleModels.Devices;

namespace WsStorageCoreTests.Views.ViewScaleModels.Devices;

[TestFixture]
public sealed class ViewDevicesRepositoryTests : ViewRepositoryTests
{
    private IViewDeviceRepository ViewDeviceRepository { get; } = new WsSqlViewDeviceRepository();

    protected override IResolveConstraint SortOrderValue => Is
        .Ordered.By(nameof(WsSqlViewDeviceModel.Name)).Ascending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewDeviceModel> items = ViewDeviceRepository.GetList(SqlCrudConfig);
            PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}