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

    protected void PrintViewRecords<T>(List<T> items) where T : class
    {
        Assert.That(items.Any(), Is.True, $"{WsLocaleCore.Tests.NoDataInDb}!");
        Assert.That(items, SortOrderValue, $"{WsLocaleCore.Tests.SortingError}!");
        TestContext.WriteLine($"Print {items.Count} records.");
        foreach (T item in items)
            TestContext.WriteLine(WsSqlQueries.TrimQuery(item.ToString() ?? string.Empty));
    }
}