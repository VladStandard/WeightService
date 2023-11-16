namespace WsStorageCoreTests.Tables.Common;

public class TableRepositoryTests
{
    protected SqlCrudConfigModel SqlCrudConfig { get; private set; }
    protected List<EnumConfiguration> DefaultConfigurations { get; }

    public TableRepositoryTests()
    {
        DefaultConfigurations = new() { EnumConfiguration.DevelopVs, EnumConfiguration.ReleaseVs };
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
        Assert.That(list.Any(), Is.True, $"{LocaleCore.Tests.NoDataInDb}!");
        Assert.That(list, SortOrderValue, $"{LocaleCore.Tests.SortingError}!");

        TestContext.WriteLine($"{LocaleCore.Tests.Print} {list.Count} {LocaleCore.Tests.Records}.");

        foreach (T item in list)
        {
            TestContext.WriteLine(SqlQueries.TrimQuery(item.ToString()));

            ValidationResult validationResult = SqlValidationUtils.GetValidationResult(item, true);
            Assert.That(validationResult.IsValid, Is.True, validationResult.ToString());
        }
    }
}