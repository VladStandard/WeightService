using Ws.StorageCore.Entities.SchemaRef.WorkShops;

namespace Ws.StorageCoreTests.Tables.TableRefModels.WorkShops;

[TestFixture]
public sealed class WorkShopRepositoryTests : TableRepositoryTests
{
    private SqlWorkShopRepository WorkShopRepository { get; } = new();

    [Test, Order(1)]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlWorkShopEntity> items = WorkShopRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}