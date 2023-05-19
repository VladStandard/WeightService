// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
            
            Assert.IsTrue(sqlCrudConfig.IsMarked == WsSqlIsMarked.ShowAll);
            Assert.IsTrue(sqlCrudConfig.Filters.Count == 0);
            TestContext.WriteLine(sqlCrudConfig);
        });
    }

	[Test]
	public void Create_and_set_filter_show_only_hide()
	{
        Assert.DoesNotThrow(() =>
        {
			WsSqlCrudConfigModel sqlCrudConfig = new();
            
            sqlCrudConfig.IsMarked = WsSqlIsMarked.ShowOnlyHide;
            
            Assert.IsTrue(sqlCrudConfig.IsMarked == WsSqlIsMarked.ShowOnlyHide);
            Assert.IsTrue(sqlCrudConfig.Filters.Count == 1);
            TestContext.WriteLine(sqlCrudConfig);
        });
    }

	[Test]
	public void Create_and_set_filter_show_only_actual()
	{
        Assert.DoesNotThrow(() =>
        {
			WsSqlCrudConfigModel sqlCrudConfig = new();
            
            sqlCrudConfig.IsMarked = WsSqlIsMarked.ShowOnlyActual;
            
            Assert.IsTrue(sqlCrudConfig.IsMarked == WsSqlIsMarked.ShowOnlyActual);
            Assert.IsTrue(sqlCrudConfig.Filters.Count == 1);
            TestContext.WriteLine(sqlCrudConfig);
        });
    }

    #endregion
}