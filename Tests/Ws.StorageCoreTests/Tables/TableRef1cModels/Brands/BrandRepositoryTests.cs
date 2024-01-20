using Ws.Domain.Models.Entities.Ref1c;
using Ws.StorageCore.Entities.Ref1c.Brands;

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
            IEnumerable<BrandEntity> items = BrandRepository.GetEnumerable();
            ParseRecords(items);
        });
    }
}