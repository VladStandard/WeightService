// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageContextTests.View;

[TestFixture]
public sealed class GetListViewTests
{
    [Test]
    public void Get_view_logs_memories()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewLogMemoryModel> items = 
                WsTestsUtils.DataTests.ContextManager.ViewLogMemoryRepository.GetList(new());
            Assert.IsTrue(items.Any());
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
    
    [Test]
    public void Get_view_tables_sizes()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewTableSizeModel> items = 
                WsTestsUtils.DataTests.ContextManager.ViewTableSizeRepository.GetList(200);
            Assert.IsTrue(items.Any());
            WsTestsUtils.DataTests.PrintTopRecords(items);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
    
    [Test]
    public void Get_view_plus_scales()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewPluLineModel> items = 
                WsTestsUtils.DataTests.ContextManager.ViewPluLineRepository.GetList(0 ,200);
            Assert.IsTrue(items.Any());
            WsTestsUtils.DataTests.PrintTopRecords(items);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
    
    [Test]
    public void Get_view_plus_storage_methods()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewPluStorageMethodModel> items = 
                WsTestsUtils.DataTests.ContextManager.ViewPluStorageMethodRepository.GetList(200);
            Assert.IsTrue(items.Any());
            WsTestsUtils.DataTests.PrintTopRecords(items);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
    
    [Test]
    public void Get_view_plus_nesting()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewPluNestingModel> items = 
                WsTestsUtils.DataTests.ContextManager.ViewPluNestingRepository.GetList();
            Assert.IsTrue(items.Any());
            WsTestsUtils.DataTests.PrintTopRecords(items);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}