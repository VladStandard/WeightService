using Ws.Database.Core.Sessions;
using Ws.Domain.Models.Common;

namespace Ws.StorageCoreTests.Tables.Common;

public class TableRepositoryTests
{
    public TableRepositoryTests()
    {
        NHibernateHelper.SetSessionFactory();
    }

    [SetUp]
    public void SetUp()
    {
    }

    protected virtual IResolveConstraint SortOrderValue => Is.Ordered.Ascending;

    protected void ParseRecords<T>(IEnumerable<T> items) where T : EntityBase, new()
    {
        List<T> list = items.ToList();

        Assert.That(list.Any(), Is.True, "Нет данных в бд");
        Assert.That(list, SortOrderValue, "Ошибка сортировки");

        TestContext.WriteLine($"Выведено {list.Count} записей.");
    }

    protected void AssertAction(Action action)
    {
        Assert.DoesNotThrow(() => {
            action();
            TestContext.WriteLine();
        });
    }
}