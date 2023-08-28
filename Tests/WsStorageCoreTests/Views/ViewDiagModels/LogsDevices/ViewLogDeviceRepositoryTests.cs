namespace WsStorageCoreTests.Views.ViewDiagModels.LogsDevices;

[TestFixture]
public sealed class ViewLogDeviceRepositoryTests : ViewRepositoryTests
{
    private IViewLogDeviceRepository ViewLogDeviceRepository { get; } = new WsSqlViewLogDeviceRepository();
    private WsSqlAppRepository AppRepository { get; } = new();
    private WsSqlDeviceRepository DeviceRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue =>
        Is.Ordered.By(nameof(WsSqlViewLogDeviceModel.CreateDt)).Descending;

    private List<WsSqlAppModel> GetApps() =>
        AppRepository.GetList(new WsSqlCrudConfigModel(SqlCrudConfig) { SelectTopRowsCount = 0 });

    [Test]
    public void Get_list()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlViewLogDeviceModel> items = ViewLogDeviceRepository.GetList(SqlCrudConfig);
            PrintViewRecords(items);
        }, false, AllConfigurations);
    }

    [Test]
    public void Get_list_for_records()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlViewLogDeviceModel> items = ViewLogDeviceRepository.GetList(10);
            PrintViewRecords(items);
        }, false, AllConfigurations);
    }

    [Test]
    public void Get_list_for_all_apps()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            foreach (WsSqlAppModel app in GetApps())
            {
                IEnumerable<WsSqlViewLogDeviceModel> items = ViewLogDeviceRepository.GetList(
                    app.Name, DeviceRepository.GetCurrentDevice().Name, WsSqlEnumTimeInterval.All, 10);
                if (items.Any())
                    PrintViewRecords(items);
                else
                    TestContext.WriteLine($"{WsLocaleCore.Tests.NoDataFor} '{app.Name}'!");
            }
        }, false, AllConfigurations);
    }

    [Test]
    public void Get_list_for_all_apps_today()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            foreach (WsSqlAppModel app in GetApps())
            {
                IEnumerable<WsSqlViewLogDeviceModel> items = ViewLogDeviceRepository.GetList(
                    app.Name, DeviceRepository.GetCurrentDevice().Name, WsSqlEnumTimeInterval.Today, 10);
                if (items.Any())
                    PrintViewRecords(items);
                else
                    TestContext.WriteLine($"{WsLocaleCore.Tests.NoDataFor} '{app.Name}'!");
            }
        }, false, AllConfigurations);
    }
}