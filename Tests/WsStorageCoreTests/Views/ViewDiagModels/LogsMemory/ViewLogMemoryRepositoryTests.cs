// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Views.ViewDiagModels.LogsMemory;

[TestFixture]
public sealed class ViewLogMemoryRepositoryTests : ViewRepositoryTests
{
    private IViewLogMemoryRepository ViewLogMemoryRepository { get; } = new WsSqlViewLogMemoryRepository();
    private WsSqlAppRepository AppRepository { get; } = new();
    private WsSqlDeviceRepository DeviceRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue =>
        Is.Ordered.By(nameof(WsSqlViewLogMemoryModel.CreateDt)).Descending;

    private List<WsSqlAppModel> GetApps() =>
        AppRepository.GetList(new(SqlCrudConfig) { SelectTopRowsCount = 0 });

    [Test]
    public void Get_list()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            // Метод теперь не падает, т.к. проксирующий метод GetList теперь пересылает на другой GetList.
            List<WsSqlViewLogMemoryModel> items = ViewLogMemoryRepository.GetList(SqlCrudConfig);
            PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }

    [Test]
    public void Get_list_for_records()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewLogMemoryModel> items = ViewLogMemoryRepository.GetList(10);
            PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }

    [Test]
    public void Get_list_for_all_apps()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            foreach (WsSqlAppModel app in GetApps())
            {
                List<WsSqlViewLogMemoryModel> items = ViewLogMemoryRepository.GetList(
                    app.Name, DeviceRepository.GetCurrentDevice().Name, WsSqlEnumTimeInterval.All, 10);
                if (items.Any())
                    PrintViewRecords(items);
                else
                    TestContext.WriteLine($"{WsLocaleCore.Tests.NoDataFor} '{app.Name}'!");
            }
        }, false, DefaultPublishTypes);
    }

    [Test]
    public void Get_list_for_all_apps_today()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            foreach (WsSqlAppModel app in GetApps())
            {
                List<WsSqlViewLogMemoryModel> items = ViewLogMemoryRepository.GetList(
                    app.Name, DeviceRepository.GetCurrentDevice().Name, WsSqlEnumTimeInterval.Today, 10);
                if (items.Any())
                    PrintViewRecords(items);
                else
                    TestContext.WriteLine($"{WsLocaleCore.Tests.NoDataFor} '{app.Name}'!");
            }
        }, false, DefaultPublishTypes);
    }
}