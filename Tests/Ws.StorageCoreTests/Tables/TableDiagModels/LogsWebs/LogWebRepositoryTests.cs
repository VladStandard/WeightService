using Ws.StorageCoreTests.Tables.Common;
using Ws.StorageCore.Entities.SchemaDiag.LogsWebs;

namespace Ws.StorageCoreTests.Tables.TableDiagModels.LogsWebs;

[TestFixture]
public sealed class LogWebsRepositoryTests : TableRepositoryTests
{
    private SqlLogWebRepository LogWebRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(SqlEntityBase.CreateDt)).Descending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlLogWebEntity> items = LogWebRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}