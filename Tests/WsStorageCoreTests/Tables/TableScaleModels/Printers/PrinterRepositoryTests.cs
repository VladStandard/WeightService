using WsStorageCore.Entities.SchemaScale.Printers;
namespace WsStorageCoreTests.Tables.TableScaleModels.Printers;

[TestFixture]
public sealed class PrinterRepositoryTests : TableRepositoryTests
{
    private WsSqlPrinterRepository PrinterRepository { get; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlPrinterEntity> items = PrinterRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}