using WsStorageCore.Entities.SchemaRef.Printers;

namespace WsStorageCoreTests.Tables.TableRefModels.Printers;

[TestFixture]
public sealed class PrinterRepositoryTests : TableRepositoryTests
{
    private SqlPrinterRepository PrinterRepository { get; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlPrinterEntity> items = PrinterRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}