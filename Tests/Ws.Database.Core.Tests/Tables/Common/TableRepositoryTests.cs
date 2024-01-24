using Ws.Database.Core.Helpers;
using Ws.Database.Core.Models;
using Ws.Domain.Models.Common;

namespace Ws.StorageCoreTests.Tables.Common;

public class TableRepositoryTests
{    
    protected SqlCrudConfigModel SqlCrudConfig { get; private set; }
    
    public TableRepositoryTests()
    {
        SqlCrudConfig = new();
        SqlCoreHelper.Instance.SetSessionFactory();
    }

    [SetUp]
    public void SetUp()
    {
        SqlCrudConfig = new() { SelectTopRowsCount = 10 };
    }

    protected virtual IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(EntityBase.Name)).Ascending;

    protected void ParseRecords<T>(IEnumerable<T> items) where T : EntityBase, new()
    {
        List<T> list = items.ToList();
        
        Assert.That(list.Any(), Is.True, "Нет данных в бд");
        Assert.That(list, SortOrderValue, "Ошибка сортировки");

        TestContext.WriteLine($"Выведено {list.Count} записей.");
    }
    
    protected void AssertAction(Action action)
    {
        Assert.DoesNotThrow(() =>
        {
            action();
            TestContext.WriteLine();
        });
    }
}