using Ws.Domain.Models.Entities.Diag;
using Ws.StorageCore.Entities.Diag.LogWebs;

namespace Ws.StorageCoreTests.Tables.TableDiagModels.LogsWebs;

[TestFixture]
public sealed class LogWebsRepositoryTests : TableRepositoryTests
{
    private SqlLogWebRepository LogWebRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(LogWebEntity.CreateDt)).Descending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<LogWebEntity> items = LogWebRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        });
    }
}