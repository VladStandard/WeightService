using WsStorageCore.Entities.SchemaRef1c.Brands;

namespace WsStorageCoreTests.Tables.TableScaleModels.Brands;

[TestFixture]
public sealed class BrandRepositoryTests : TableRepositoryTests
{
    private SqlBrandRepository BrandRepository { get; } = new();

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlBrandEntity> items = BrandRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}