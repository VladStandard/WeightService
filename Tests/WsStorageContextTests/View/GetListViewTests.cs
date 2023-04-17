// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Enums;
using WsStorage.ViewDiagModels;

namespace WsStorageContextTests.View;

[TestFixture]
internal class GetListViewTests
{
    [Test]
    public void Get_view_logs_memories()
    {
        DataCoreTestsUtils.DataCore.AssertAction(() =>
        {
            List<WsSqlViewLogMemory> items = DataCoreTestsUtils.DataCore.DataContext.GetListViewLogsMemories(200);
            Assert.IsTrue(items.Any());
            DataCoreTestsUtils.DataCore.PrintTopRecords(items, 10);
        }, false, new() { WsConfiguration.DevelopVS, WsConfiguration.ReleaseVS });
    }
    
    [Test]
    public void Get_view_tables_sizes()
    {
        DataCoreTestsUtils.DataCore.AssertAction(() =>
        {
            List<WsSqlViewTableSizeMemory> items = DataCoreTestsUtils.DataCore.DataContext.GetListViewTablesSizes(200);
            Assert.IsTrue(items.Any());
            DataCoreTestsUtils.DataCore.PrintTopRecords(items);
        }, false, new() { WsConfiguration.DevelopVS, WsConfiguration.ReleaseVS });
    }
}