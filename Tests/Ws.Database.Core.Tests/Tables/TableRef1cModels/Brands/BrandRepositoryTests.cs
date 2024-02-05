using Ws.Database.Core.Entities.Ref1c.Brands;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.StorageCoreTests.Tables.TableRef1cModels.Brands;

[TestFixture]
public sealed class BrandRepositoryTests : TableRepositoryTests
{
    private SqlBrandRepository BrandRepository { get; } = new();

    [Test]
    public void GetList()
    {
        AssertAction(() =>
        {
            IEnumerable<BrandEntity> items = BrandRepository.GetAll();
            ParseRecords(items);
        });
    }
}