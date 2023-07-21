using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleModels.PrintersTypes;

[TestFixture]
public sealed class PrinterTypeRepositoryTests : TableRepositoryTests
{
    private WsSqlPrinterTypeRepository PrinterTypeRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPrinterTypeModel> items = PrinterTypeRepository.GetList(SqlCrudConfig);
            WsTestsUtils.DataTests.ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
}