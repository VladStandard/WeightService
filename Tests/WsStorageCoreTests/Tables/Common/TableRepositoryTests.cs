namespace WsStorageCoreTests.Tables.Common;

public class TableRepositoryTests
{
    protected SqlCrudConfigModel SqlCrudConfig { get; private set; }
    protected List<WsEnumConfiguration> DefaultConfigurations { get; }

    public TableRepositoryTests()
    {
        DefaultConfigurations = new() { WsEnumConfiguration.DevelopVs, WsEnumConfiguration.ReleaseVs };
        SqlCrudConfig = new();
    }

    [SetUp]
    public void SetUp()
    {
        SqlCrudConfig = new() { SelectTopRowsCount = 10, IsResultOrder = true };
    }

    protected virtual IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(SqlEntityBase.Name)).Ascending;

    protected void ParseRecords<T>(IEnumerable<T> items) where T : SqlEntityBase, new()
    {
        List<T> list = items.ToList();
        Assert.That(list.Any(), Is.True, $"{WsLocaleCore.Tests.NoDataInDb}!");
        Assert.That(list, SortOrderValue, $"{WsLocaleCore.Tests.SortingError}!");

        TestContext.WriteLine($"{WsLocaleCore.Tests.Print} {list.Count} {WsLocaleCore.Tests.Records}.");

        foreach (T item in list)
        {
            TestContext.WriteLine(SqlQueries.TrimQuery(item.ToString()));

            ValidationResult validationResult = SqlValidationUtils.GetValidationResult(item, true);
            Assert.That(validationResult.IsValid, Is.True, validationResult.ToString());
        }
    }
}