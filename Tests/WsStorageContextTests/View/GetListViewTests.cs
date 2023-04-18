// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageContextTests.View;

[TestFixture]
public sealed class GetListViewTests
{
    [Test]
    public void Get_view_logs_memories()
    {
        WsTestsUtils.DataCore.AssertAction(() =>
        {
            List<WsSqlViewLogMemory> items = WsTestsUtils.DataCore.DataContext.GetListViewLogsMemories(200);
            Assert.IsTrue(items.Any());
            WsTestsUtils.DataCore.PrintTopRecords(items, 10);
        }, false, new() { WsConfiguration.DevelopVS, WsConfiguration.ReleaseVS });
    }
    
    [Test]
    public void Get_view_tables_sizes()
    {
        WsTestsUtils.DataCore.AssertAction(() =>
        {
            List<WsSqlViewTableSizeMemory> items = WsTestsUtils.DataCore.DataContext.GetListViewTablesSizes(200);
            Assert.IsTrue(items.Any());
            WsTestsUtils.DataCore.PrintTopRecords(items);
        }, false, new() { WsConfiguration.DevelopVS, WsConfiguration.ReleaseVS });
    }
}