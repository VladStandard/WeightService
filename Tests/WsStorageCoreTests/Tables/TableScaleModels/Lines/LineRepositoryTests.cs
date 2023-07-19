namespace WsStorageCoreTests.Tables.TableScaleModels.Lines;

[TestFixture]
public sealed class LineRepositoryTests : TableRepositoryTests
{
    private WsSqlLineRepository LineRepository { get; set; } = new();
   
    [Test, Order(1)]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlScaleModel> items = LineRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
    
    [Test, Order(3)]
    public void GetById()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            
            const long id = 47; //Aleksandrov stand
            WsSqlScaleModel line = LineRepository.GetItemById(id);
            
            Assert.That(line.IsExists, Is.True);
            Assert.That(line.IdentityValueId, Is.EqualTo(id));

            TestContext.WriteLine($"Get item success: {line.Description}: {line.IdentityValueId}");
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}