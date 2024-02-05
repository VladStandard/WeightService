using Ws.Database.Core.Helpers;
using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.StorageCoreTests.Tables.Common;

public class TableRepositoryTests
{    
    public TableRepositoryTests()
    {
        SqlCoreHelper.Instance.SetSessionFactory();
    }

    [SetUp]
    public void SetUp()
    {
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