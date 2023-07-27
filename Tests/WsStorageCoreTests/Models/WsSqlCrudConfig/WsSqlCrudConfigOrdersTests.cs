// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Models.WsSqlCrudConfig;

[TestFixture]
public sealed class WsSqlCrudConfigOrdersTests
{
    [Test]
    public void CheckAddOrder()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlCrudConfigModel sqlCrudConfig = new();
            sqlCrudConfig.AddOrder(new("data № 1", WsSqlEnumOrder.Asc));
            
            Assert.That(sqlCrudConfig.Orders, Has.Count.EqualTo(1));
            
            TestContext.WriteLine(sqlCrudConfig);
        });
    }
    
    [Test]
    public void CheckAddManyOrder()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlCrudConfigModel sqlCrudConfig = new();
            sqlCrudConfig.AddOrder(new("Test № 1", WsSqlEnumOrder.Desc));
            sqlCrudConfig.AddOrders(new()
            {
                new("Test № 2", WsSqlEnumOrder.Asc),
                new("Test № 3", WsSqlEnumOrder.Asc),
            });
            
            Assert.That(sqlCrudConfig.Orders, Has.Count.EqualTo(3));
            
            TestContext.WriteLine(sqlCrudConfig);
        });
    }
    
    [Test]
    public void CheckAddEqualOrder()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlCrudConfigModel sqlCrudConfig = new();
            sqlCrudConfig.AddOrder(new("Test № 1", WsSqlEnumOrder.Desc));
            sqlCrudConfig.AddOrders(new()
            {
                new("Test № 1", WsSqlEnumOrder.Asc),
                new("Test № 3", WsSqlEnumOrder.Asc),
            });
            
            Assert.That(sqlCrudConfig.Orders, Has.Count.EqualTo(2));
            
            TestContext.WriteLine(sqlCrudConfig);
        });
    }
    
    [Test]
    public void CheckDeleteOneOrder()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlCrudConfigModel sqlCrudConfig = new();
            sqlCrudConfig.AddOrder(new("Test № 1", WsSqlEnumOrder.Desc));
            sqlCrudConfig.RemoveOrder(new("Test № 1", WsSqlEnumOrder.Desc));

            Assert.That(sqlCrudConfig.Filters, Has.Count.EqualTo(0));
            
            TestContext.WriteLine(sqlCrudConfig);
        });
    }
    
    [Test]
    public void CheckDeleteManyOrder()
    {
        Assert.DoesNotThrow(() =>
        {   
            WsSqlCrudConfigModel sqlCrudConfig = new();
            sqlCrudConfig.AddOrders(new()
            {
                new("Test № 1", WsSqlEnumOrder.Desc),
                new("Test № 2", WsSqlEnumOrder.Desc),
                new("Test № 3", WsSqlEnumOrder.Asc),
            });
                
            sqlCrudConfig.RemoveOrders(new()
            {
                new("Test № 2", WsSqlEnumOrder.Desc),
                new("Test № 3", WsSqlEnumOrder.Asc),
            });
            
            Assert.That(sqlCrudConfig.Orders, Has.Count.EqualTo(1));
            
            TestContext.WriteLine(sqlCrudConfig);
        });
    }
    
    [Test]
    public void  CheckDeleteAllOrder()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlCrudConfigModel sqlCrudConfig = new();
            sqlCrudConfig.AddOrders(new()
            {
                new("Test № 1", WsSqlEnumOrder.Desc),
                new("Test № 2", WsSqlEnumOrder.Asc),
                new("Test № 3", WsSqlEnumOrder.Asc),
            });
            
            sqlCrudConfig.ClearOrders();
            Assert.That(sqlCrudConfig.Orders, Has.Count.EqualTo(0));
            
            TestContext.WriteLine(sqlCrudConfig);
        });
    }
}