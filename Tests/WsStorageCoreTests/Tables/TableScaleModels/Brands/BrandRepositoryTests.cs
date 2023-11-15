using WsStorageCore.Entities.SchemaRef1c.Brands;

namespace WsStorageCoreTests.Tables.TableScaleModels.Brands;

[TestFixture]
public sealed class BrandRepositoryTests : TableRepositoryTests
{
    private WsSqlBrandRepository BrandRepository { get; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlBrandEntity> items = BrandRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}