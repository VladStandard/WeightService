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
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}