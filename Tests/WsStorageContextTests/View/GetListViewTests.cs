// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.ViewRefModels;

namespace WsStorageContextTests.View;

[TestFixture]
public sealed class GetListViewTests
{
    [Test]
    public void Get_view_logs_memories()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewLogMemoryModel> items = WsTestsUtils.DataTests.ContextManager.ContextView.GetListViewLogsMemories(200);
            Assert.IsTrue(items.Any());
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, new() { WsConfiguration.DevelopVS, WsConfiguration.ReleaseVS });
    }
    
    [Test]
    public void Get_view_tables_sizes()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewTableSizeMemoryModel> items = WsTestsUtils.DataTests.ContextManager.ContextView.GetListViewTablesSizes(200);
            Assert.IsTrue(items.Any());
            WsTestsUtils.DataTests.PrintTopRecords(items);
        }, false, new() { WsConfiguration.DevelopVS, WsConfiguration.ReleaseVS });
    }
    
    [Test]
    public void Get_view_plus_scales()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewPluScaleModel> items = WsTestsUtils.DataTests.ContextManager.ContextView.GetListViewPlusScales(200);
            Assert.IsTrue(items.Any());
            WsTestsUtils.DataTests.PrintTopRecords(items);
        }, false, new() { WsConfiguration.DevelopVS, WsConfiguration.ReleaseVS });
    }
}