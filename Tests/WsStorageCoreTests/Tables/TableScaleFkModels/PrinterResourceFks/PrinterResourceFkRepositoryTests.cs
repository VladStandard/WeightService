namespace WsStorageCoreTests.Tables.TableScaleFkModels.PrinterResourceFks;

// TODO: printer resources
public sealed class PrinterResourcesFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPrinterResourceFkRepository PrinterResourceFkRepository { get; set; } = new();
    
    // [Test]
    // public void GetList()
    // {
    //     WsTestsUtils.DataTests.AssertAction(() =>
    //     {
    //         List<WsSqlPrinterResourceFkModel> items = PrinterResourceFkRepository.GetList(SqlCrudConfig);
    //         Assert.That(items.Any(), Is.True);
    //         WsTestsUtils.DataTests.PrintTopRecords(items, 10);
    //     }, false, DefaultPublishTypes);
    // }
}