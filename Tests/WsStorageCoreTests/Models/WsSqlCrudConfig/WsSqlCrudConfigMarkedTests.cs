// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Models.WsSqlCrudConfig;

[TestFixture]
public sealed class WsSqlCrudConfigModelMarkedTests
{
    [Test]
	public void CheckShowAll()
	{
        Assert.DoesNotThrow(() =>
        {
			WsSqlCrudConfigModel sqlCrudConfig = new();
            
            Assert.That(sqlCrudConfig.IsMarked, Is.EqualTo(WsSqlEnumIsMarked.ShowAll));
            Assert.That(sqlCrudConfig.Filters, Is.Empty);
            
            TestContext.WriteLine(sqlCrudConfig);
        });
    }

	[Test]
	public void CheckShowHide()
	{
        Assert.DoesNotThrow(() =>
        {
			WsSqlCrudConfigModel sqlCrudConfig = new() { IsMarked = WsSqlEnumIsMarked.ShowOnlyHide };

            Assert.That(sqlCrudConfig.IsMarked, Is.EqualTo(WsSqlEnumIsMarked.ShowOnlyHide));
            Assert.That(sqlCrudConfig.Filters, Has.Count.EqualTo(1));
            
            TestContext.WriteLine(sqlCrudConfig);
        });
    }

	[Test]
	public void CheckShowActual()
	{
        Assert.DoesNotThrow(() =>
        {
			WsSqlCrudConfigModel sqlCrudConfig = new() { IsMarked = WsSqlEnumIsMarked.ShowOnlyActual };

            Assert.That(sqlCrudConfig.IsMarked, Is.EqualTo(WsSqlEnumIsMarked.ShowOnlyActual));
            Assert.That(sqlCrudConfig.Filters, Has.Count.EqualTo(1));
            
            TestContext.WriteLine(sqlCrudConfig);
        });
    }
    
    [Test]
    public void CheckChangeMarked()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlCrudConfigModel sqlCrudConfig = new() { IsMarked = WsSqlEnumIsMarked.ShowOnlyActual };
           
            sqlCrudConfig.IsMarked = WsSqlEnumIsMarked.ShowOnlyHide;
            Assert.That(sqlCrudConfig.IsMarked, Is.EqualTo(WsSqlEnumIsMarked.ShowOnlyHide));
            Assert.That(sqlCrudConfig.Filters, Has.Count.EqualTo(1));
            
            sqlCrudConfig.IsMarked = WsSqlEnumIsMarked.ShowAll;
            Assert.That(sqlCrudConfig.IsMarked, Is.EqualTo(WsSqlEnumIsMarked.ShowAll));
            Assert.That(sqlCrudConfig.Filters, Has.Count.EqualTo(0));
            
            TestContext.WriteLine(sqlCrudConfig);
        });
    }
}