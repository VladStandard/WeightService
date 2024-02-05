using Ws.Database.Core.Entities.Diag.LogWebs;
using Ws.Domain.Models.Entities.Diag;

namespace Ws.StorageCoreTests.Tables.TableDiagModels.LogsWebs;

[TestFixture]
public sealed class LogWebsRepositoryTests : TableRepositoryTests
{
    private SqlLogWebRepository LogWebRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(LogWebEntity.CreateDt)).Descending;

    [Test]
    public void GetList()
    {
        // AssertAction(() =>
        // {
        //     IEnumerable<LogWebEntity> items = LogWebRepository.GetList(SqlCrudConfig);
        //     ParseRecords(items);
        // });
    }
}