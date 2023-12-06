using Ws.Shared.Enums;

namespace Ws.StorageCoreTests.Views;

public class ViewRepositoryTests
{
    protected SqlCrudConfigModel SqlCrudConfig { get; private set; }
    protected List<EnumConfiguration> AllConfigurations { get; }
    protected virtual IResolveConstraint SortOrderValue => throw new NotImplementedException();

    public ViewRepositoryTests()
    {
        SqlCrudConfig = new();
        AllConfigurations = new() { EnumConfiguration.DevelopVs, EnumConfiguration.ReleaseVs };
    }

    [SetUp]
    public void SetUp()
    {
        SqlCrudConfig = new() { SelectTopRowsCount = 10 };
    }

    protected void PrintViewRecords<T>(IEnumerable<T> items) where T : class
    {
        List<T> list = items.ToList();
        Assert.That(list.Any(), Is.True, $"{LocaleCore.Tests.NoDataInDb}!");
        Assert.That(list, SortOrderValue, $"{LocaleCore.Tests.SortingError}!");
        TestContext.WriteLine($"Print {list.Count} records.");
        foreach (T item in list)
            TestContext.WriteLine(SqlQueries.TrimQuery(item.ToString() ?? string.Empty));
    }
}