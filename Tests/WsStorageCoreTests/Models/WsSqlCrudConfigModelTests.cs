// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Common;

namespace WsStorageCoreTests.Models;

[TestFixture]
public sealed class WsSqlCrudConfigModelTests
{
	#region Public and private methods

	[Test]
	public void Create_and_set_filter_show_all()
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
	public void Create_and_set_filter_show_only_hide()
	{
        Assert.DoesNotThrow(() =>
        {
			WsSqlCrudConfigModel sqlCrudConfig = new();
            
            sqlCrudConfig.IsMarked = WsSqlEnumIsMarked.ShowOnlyHide;
            
            Assert.That(sqlCrudConfig.IsMarked, Is.EqualTo(WsSqlEnumIsMarked.ShowOnlyHide));
            Assert.That(sqlCrudConfig.Filters, Has.Count.EqualTo(1));
            
            TestContext.WriteLine(sqlCrudConfig);
        });
    }

	[Test]
	public void Create_and_set_filter_show_only_actual()
	{
        Assert.DoesNotThrow(() =>
        {
			WsSqlCrudConfigModel sqlCrudConfig = new();
            
            sqlCrudConfig.IsMarked = WsSqlEnumIsMarked.ShowOnlyActual;
            
            Assert.That(sqlCrudConfig.IsMarked, Is.EqualTo(WsSqlEnumIsMarked.ShowOnlyActual));
            Assert.That(sqlCrudConfig.Filters, Has.Count.EqualTo(1));
            
            TestContext.WriteLine(sqlCrudConfig);
        });
    }

    #endregion
}