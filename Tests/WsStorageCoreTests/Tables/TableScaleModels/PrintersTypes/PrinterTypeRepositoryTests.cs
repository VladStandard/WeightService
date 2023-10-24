using WsStorageCore.Entities.SchemaScale.PrintersTypes;
namespace WsStorageCoreTests.Tables.TableScaleModels.PrintersTypes;

[TestFixture]
public sealed class PrinterTypeRepositoryTests : TableRepositoryTests
{
    private WsSqlPrinterTypeRepository PrinterTypeRepository { get; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPrinterTypeEntity> items = PrinterTypeRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}