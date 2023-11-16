using Ws.StorageCoreTests.Tables.Common;
using Ws.StorageCore.Entities.SchemaRef.Printers;

namespace Ws.StorageCoreTests.Tables.TableRefModels.Printers;

[TestFixture]
public sealed class PrinterRepositoryTests : TableRepositoryTests
{
    private SqlPrinterRepository PrinterRepository { get; } = new();

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlPrinterEntity> items = PrinterRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}