namespace WsStorageCoreTests.Views.ViewDiagModels.LogsDevicesAggr;

[TestFixture]
public sealed class ViewLogDeviceAggrRepositoryTests : ViewRepositoryTests
{
    private IViewLogDeviceAggrRepository ViewLogDeviceAggrRepository { get; } = new WsSqlViewLogDeviceAggrRepository();
    private WsSqlAppRepository AppRepository { get; } = new();
    private WsSqlDeviceRepository DeviceRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue =>
        Is.Ordered.By(nameof(WsSqlViewLogDeviceAggrModel.CreateDt)).Descending;

    private List<WsSqlAppModel> GetApps() =>
        AppRepository.GetList(new WsSqlCrudConfigModel(SqlCrudConfig) { SelectTopRowsCount = 0 });

    [Test]
    public void Get_list()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlViewLogDeviceAggrModel> items = ViewLogDeviceAggrRepository.GetList(SqlCrudConfig);
            PrintViewRecords(items);
        }, false, AllConfigurations);
    }

    [Test]
    public void Get_list_for_records()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlViewLogDeviceAggrModel> items = ViewLogDeviceAggrRepository.GetList(10);
            PrintViewRecords(items);
        }, false, AllConfigurations);
    }

    [Test]
    public void Get_list_for_apps_only()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            foreach (WsSqlAppModel app in GetApps())
            {
                IEnumerable<WsSqlViewLogDeviceAggrModel> items = ViewLogDeviceAggrRepository.GetListForApp(
                    app.Name, WsSqlEnumTimeInterval.All, 10);
                if (items.Any())
                    PrintViewRecords(items);
                else
                    TestContext.WriteLine($"{WsLocaleCore.Tests.NoDataFor} '{app.Name}'!");
            }
        }, false, AllConfigurations);
    }

    [Test]
    public void Get_list_for_current_device_only()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            foreach (WsSqlAppModel app in GetApps())
            {
                IEnumerable<WsSqlViewLogDeviceAggrModel> items = ViewLogDeviceAggrRepository.GetListForDevice(
                    DeviceRepository.GetCurrentDevice().Name, WsSqlEnumTimeInterval.All, 10);
                if (items.Any())
                    PrintViewRecords(items);
                else
                    TestContext.WriteLine($"{WsLocaleCore.Tests.NoDataFor} '{app.Name}'!");
            }
        }, false, AllConfigurations);
    }

    [Test]
    public void Get_list_for_all_apps_current_device()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            foreach (WsSqlAppModel app in GetApps())
            {
                IEnumerable<WsSqlViewLogDeviceAggrModel> items = ViewLogDeviceAggrRepository.GetList(
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
                IEnumerable<WsSqlViewLogDeviceAggrModel> items = ViewLogDeviceAggrRepository.GetList(
                    app.Name, DeviceRepository.GetCurrentDevice().Name, WsSqlEnumTimeInterval.Today, 10);
                if (items.Any())
                    PrintViewRecords(items);
                else
                    TestContext.WriteLine($"{WsLocaleCore.Tests.NoDataFor} '{app.Name}'!");
            }
        }, false, AllConfigurations);
    }
}