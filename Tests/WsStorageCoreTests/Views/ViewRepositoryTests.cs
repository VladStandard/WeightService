namespace WsStorageCoreTests.Views;

public class ViewRepositoryTests
{
    protected WsSqlCrudConfigModel SqlCrudConfig { get; private set; }
    protected List<WsEnumConfiguration> AllConfigurations { get; }
    protected virtual IResolveConstraint SortOrderValue => throw new NotImplementedException();

    public ViewRepositoryTests()
    {
        SqlCrudConfig = new();
        AllConfigurations = new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS };
    }

    [SetUp]
    public void SetUp()
    {
        SqlCrudConfig = new() { SelectTopRowsCount = 10 };
    }

    protected void PrintViewRecords<T>(IEnumerable<T> items) where T : class
    {
        List<T> list = items.ToList();
        Assert.That(list.Any(), Is.True, $"{WsLocaleCore.Tests.NoDataInDb}!");
        Assert.That(list, SortOrderValue, $"{WsLocaleCore.Tests.SortingError}!");
        TestContext.WriteLine($"Print {list.Count} records.");
        foreach (T item in list)
            TestContext.WriteLine(WsSqlQueries.TrimQuery(item.ToString() ?? string.Empty));
    }
}