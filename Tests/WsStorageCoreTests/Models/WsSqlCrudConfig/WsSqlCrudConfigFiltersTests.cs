// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Models.WsSqlCrudConfig;

[TestFixture]
public sealed class WsSqlCrudConfigFiltersTests
{
    [Test]
    public void CheckAddFilter()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlCrudConfigModel sqlCrudConfig = new();
            sqlCrudConfig.AddFilter(new() {Name = "Test № 1", Comparer = WsSqlEnumFieldComparer.Equal, Value = "data"});
            
            Assert.That(sqlCrudConfig.Filters, Has.Count.EqualTo(1));
            
            TestContext.WriteLine(sqlCrudConfig);
        });
    }
    
    [Test]
    public void CheckAddManyFilters()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlCrudConfigModel sqlCrudConfig = new();
            sqlCrudConfig.AddFilter(new() {Name = "Test № 1", Comparer = WsSqlEnumFieldComparer.Equal, Value = "data"});
            sqlCrudConfig.AddFilters(new()
            {
                new() {Name = "Test № 2", Comparer = WsSqlEnumFieldComparer.Equal, Value = "data2"},
                new() {Name = "Test № 3", Comparer = WsSqlEnumFieldComparer.Equal, Value = "data3"},
            });
            
            Assert.That(sqlCrudConfig.Filters, Has.Count.EqualTo(3));
            
            TestContext.WriteLine(sqlCrudConfig);
        });
    }
    
    [Test]
    public void CheckAddEqualFilters()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlCrudConfigModel sqlCrudConfig = new();
            sqlCrudConfig.AddFilter(new() {Name = "Test № 1", Comparer = WsSqlEnumFieldComparer.Equal, Value = "data"});
            sqlCrudConfig.AddFilters(new()
            {
                new() {Name = "Test № 1", Comparer = WsSqlEnumFieldComparer.Equal, Value = "data4"},
                new() {Name = "Test № 1", Comparer = WsSqlEnumFieldComparer.Less, Value = "data56"},
            });
            
            Assert.That(sqlCrudConfig.Filters, Has.Count.EqualTo(2));
            
            TestContext.WriteLine(sqlCrudConfig);
        });
    }
    
    [Test]
    public void CheckDeleteOneFilter()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlCrudConfigModel sqlCrudConfig = new();
            sqlCrudConfig.AddFilter(new() {Name = "Test № 1", Comparer = WsSqlEnumFieldComparer.Equal, Value = "data"});
            sqlCrudConfig.RemoveFilter(new() {Name = "Test № 1", Comparer = WsSqlEnumFieldComparer.Equal, Value = "data"});
            
            Assert.That(sqlCrudConfig.Filters, Has.Count.EqualTo(0));
            
            TestContext.WriteLine(sqlCrudConfig);
        });
    }
    
    [Test]
    public void CheckDeleteManyFilters()
    {
        Assert.DoesNotThrow(() =>
        {   
            WsSqlCrudConfigModel sqlCrudConfig = new();
            sqlCrudConfig.AddFilter(new() {Name = "Test № 1", Comparer = WsSqlEnumFieldComparer.Equal, Value = "data"});
            sqlCrudConfig.AddFilters(new()
            {
                new() {Name = "Test № 2", Comparer = WsSqlEnumFieldComparer.Equal, Value = "data2"},
                new() {Name = "Test № 3", Comparer = WsSqlEnumFieldComparer.Equal, Value = "data3"},
            });

            sqlCrudConfig.RemoveFilters(new()
            {
                new() {Name = "Test № 2", Comparer = WsSqlEnumFieldComparer.Equal, Value = "data2"},
                new() {Name = "Test № 3", Comparer = WsSqlEnumFieldComparer.Equal, Value = "data3"},
            });
            
            Assert.That(sqlCrudConfig.Filters, Has.Count.EqualTo(1));
            
            TestContext.WriteLine(sqlCrudConfig);
        });
    }
    
    [Test]
    public void  CheckDeleteAllFilters()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlCrudConfigModel sqlCrudConfig = new();
            sqlCrudConfig.AddFilter(new() {Name = "Test № 1", Comparer = WsSqlEnumFieldComparer.Equal, Value = "data"});
            sqlCrudConfig.AddFilters(new()
            {
                new() {Name = "Test № 1", Comparer = WsSqlEnumFieldComparer.Equal, Value = "data4"},
                new() {Name = "Test № 1", Comparer = WsSqlEnumFieldComparer.Less, Value = "data56"},
            });
            
            sqlCrudConfig.ClearFilters();
            Assert.That(sqlCrudConfig.Filters, Has.Count.EqualTo(0));
            
            TestContext.WriteLine(sqlCrudConfig);
        });
    }
}