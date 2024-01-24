using Ws.Database.Core.Entities.Diag.TableSizes;
using Ws.Domain.Models.Entities.Diag;

namespace Ws.StorageCoreTests.Tables.TableDiagModels.ViewTableSize;

[TestFixture]
public sealed class ViewTableSizeRepositoryTests : TableRepositoryTests
{
    private SqlViewTableSizeRepository TableSizeRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        AssertAction(() =>
        {
            IEnumerable<TableSizeEntity> items = TableSizeRepository.GetEnumerable();
            ParseRecords(items);
        });
    }
}