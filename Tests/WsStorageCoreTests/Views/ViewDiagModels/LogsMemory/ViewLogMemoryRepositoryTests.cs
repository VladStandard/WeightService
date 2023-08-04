// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsLocalizationCore.Utils;

namespace WsStorageCoreTests.Views.ViewDiagModels.LogsMemory;

[TestFixture]
public sealed class ViewLogMemoryRepositoryTests : ViewRepositoryTests
{
    private IViewLogMemoryRepository ViewLogMemoryRepository { get; } = new WsSqlViewLogMemoryRepository();
    private WsSqlAppRepository AppRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue =>
        Is.Ordered.By(nameof(WsSqlViewLogMemoryModel.CreateDt)).Descending;

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
    public void Get_list_for_all_apps()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlCrudConfigModel sqlCrudConfig = new(SqlCrudConfig) { SelectTopRowsCount = 0 };
            List<WsSqlAppModel> apps = AppRepository.GetList(sqlCrudConfig);
            foreach (WsSqlAppModel app in apps)
            {
                List<WsSqlViewLogMemoryModel> items = ViewLogMemoryRepository.GetList(SqlCrudConfig.SelectTopRowsCount, app.Name);
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
            WsSqlCrudConfigModel sqlCrudConfig = new(SqlCrudConfig) { SelectTopRowsCount = 0 };
            List<WsSqlAppModel> apps = AppRepository.GetList(sqlCrudConfig);
            foreach (WsSqlAppModel app in apps)
            {
                List<WsSqlViewLogMemoryModel> items = ViewLogMemoryRepository.GetListToday(SqlCrudConfig.SelectTopRowsCount, app.Name);
                if (items.Any())
                    PrintViewRecords(items);
                else
                    TestContext.WriteLine($"{WsLocaleCore.Tests.NoDataFor} '{app.Name}'!");
            }
        }, false, DefaultPublishTypes);
    }

    [Test]
    public void Get_list_for_all_apps_month()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlCrudConfigModel sqlCrudConfig = new(SqlCrudConfig) { SelectTopRowsCount = 0 };
            List<WsSqlAppModel> apps = AppRepository.GetList(sqlCrudConfig);
            foreach (WsSqlAppModel app in apps)
            {
                List<WsSqlViewLogMemoryModel> items = ViewLogMemoryRepository.GetListMonth(SqlCrudConfig.SelectTopRowsCount, app.Name);
                if (items.Any())
                    PrintViewRecords(items);
                else
                    TestContext.WriteLine($"{WsLocaleCore.Tests.NoDataFor} '{app.Name}'!");
            }
        }, false, DefaultPublishTypes);
    }

    [Test]
    public void Get_list_for_app_device_control()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewLogMemoryModel> items = ViewLogMemoryRepository.GetListDeviceControl();
            PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }

    [Test]
    public void Get_list_for_app_device_control_today()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewLogMemoryModel> items = ViewLogMemoryRepository.GetListDeviceControlToday();
            PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }

    [Test]
    public void Get_list_for_app_device_control_month()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewLogMemoryModel> items = ViewLogMemoryRepository.GetListDeviceControlMonth();
            PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}