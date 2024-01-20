using Ws.StorageCore.Entities.SchemaRef1c.Brands;

namespace Ws.StorageCoreTests.Tables.TableRef1cModels.Brands;

[TestFixture]
public sealed class BrandRepositoryTests : TableRepositoryTests
{
    private SqlBrandRepository BrandRepository { get; } = new();

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlBrandEntity> items = BrandRepository.GetEnumerable();
            ParseRecords(items);
        });
    }
}