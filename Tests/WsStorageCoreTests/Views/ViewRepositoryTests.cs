using WsStorageCore.Utils;

namespace WsStorageCoreTests.Views;

public class ViewRepositoryTests
{
    protected WsSqlCrudConfigModel SqlCrudConfig { get; set; }
    protected List<WsEnumConfiguration> DefaultPublishTypes { get; set; }

    public ViewRepositoryTests()
    {
        SqlCrudConfig = new () { SelectTopRowsCount = 10 };
        DefaultPublishTypes = new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS };
    }
    
    protected static void PrintViewRecords<T>(List<T> items) where T : class
    {
        Assert.That(items.Any(), Is.True, "No data in database!!!");
        TestContext.WriteLine($"Print {items.Count} records.");
        foreach (T item in items)
            TestContext.WriteLine(WsSqlQueries.TrimQuery(item.ToString() ?? string.Empty));
    }
}