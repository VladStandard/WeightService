using Ws.Database.Nhibernate.Entities.Ref.Claims;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.StorageCoreTests.Tables.TableRefModels.Claims;

[TestFixture]
public sealed class ClaimRepositoryTests : TableRepositoryTests
{
    private SqlClaimRepository ClaimRepository { get; } = new();

    [Test, Order(1)]
    public void GetList()
    {
        AssertAction(() =>
        {
            IEnumerable<ClaimEntity> items = ClaimRepository.GetAll();
            ParseRecords(items);
        });
    }
}