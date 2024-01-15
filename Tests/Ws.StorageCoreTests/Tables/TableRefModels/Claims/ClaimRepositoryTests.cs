using Ws.StorageCore.Entities.SchemaRef.Claims;

namespace Ws.StorageCoreTests.Tables.TableRefModels.Claims;

[TestFixture]
public sealed class ClaimRepositoryTests : TableRepositoryTests
{
    private SqlClaimRepository ClaimRepository { get; } = new();

    [Test, Order(1)]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlClaimEntity> items = ClaimRepository.GetEnumerable();
            ParseRecords(items);
        }, false);
    }
}