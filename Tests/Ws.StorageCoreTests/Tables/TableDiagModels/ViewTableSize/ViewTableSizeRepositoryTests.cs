using Ws.Domain.Models.Entities.Diag;
using Ws.StorageCore.Entities.Diag.TableSizes;

namespace Ws.StorageCoreTests.Tables.TableDiagModels.ViewTableSize;

[TestFixture]
public sealed class ViewTableSizeRepositoryTests : TableRepositoryTests
{
    private SqlViewTableSizeRepository TableSizeRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<TableSizeEntity> items = TableSizeRepository.GetEnumerable();
            ParseRecords(items);
        });
    }
}