using Ws.Domain.Models.Entities.Ref;
using Ws.StorageCore.Entities.Ref.Claims;

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
            IEnumerable<ClaimEntity> items = ClaimRepository.GetEnumerable();
            ParseRecords(items);
        });
    }
}