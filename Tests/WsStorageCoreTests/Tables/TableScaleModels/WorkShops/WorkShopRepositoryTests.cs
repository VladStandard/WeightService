using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleModels.WorkShops;

[TestFixture]
public sealed class WorkShopRepositoryTests : TableRepositoryTests
{
    private WsSqlWorkShopRepository WorkShopRepository { get; set; } = new();
    
    [Test, Order(1)]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlWorkShopModel> items = WorkShopRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
    
    [Test, Order(3)]
    public void GetById()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            const long id = 1;
            WsSqlWorkShopModel workShop = WorkShopRepository.GetItemById(id);
            
            Assert.That(workShop.IsExists, Is.True);
            Assert.That(workShop.IdentityValueId, Is.EqualTo(id));
            
            TestContext.WriteLine($"Get item success: {workShop.Name}: {workShop.IdentityValueId}");
        }, false, DefaultPublishTypes);
    }
    
    [Test, Order(4)]
    public void GetNewItem()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlWorkShopModel app = WorkShopRepository.GetNewItem();
            Assert.That(app.IsNotExists, Is.True);
            TestContext.WriteLine($"New item: {app.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}