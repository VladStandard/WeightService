namespace Ws.StorageCoreTests.Tables.Common;

public class TableRepositoryTests
{
    protected SqlCrudConfigModel SqlCrudConfig { get; private set; }

    public TableRepositoryTests()
    {
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
        Assert.That(list.Any(), Is.True, "Нет данных в бд");
        Assert.That(list, SortOrderValue, "Ошибка сортировки");

        TestContext.WriteLine($"Выведено {list.Count} записей.");

        foreach (T item in list)
        {
            TestContext.WriteLine(SqlQueries.TrimQuery(item.ToString()));

            ValidationResult validationResult = SqlValidationUtils.GetValidationResult(item, true);
            Assert.That(validationResult.IsValid, Is.True, validationResult.ToString());
        }
    }
}