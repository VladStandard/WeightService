// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Memory;
using WsStorageCore.Common;

namespace WsStorageContextTests.DataContext;

[TestFixture]
public sealed class LogMemoryTests
{
    private static List<WsEnumConfiguration> Configurations => new() { WsEnumConfiguration.ReleaseVS, WsEnumConfiguration.DevelopVS };
    private static WsSqlCrudConfigModel SqlCrudConfigFk => new(WsSqlEnumIsMarked.ShowAll, true, false, true, false);

    [Test]
    public void DataContext_GetDbFileSizeInfos_Assert()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            MemorySizeModel memorySize = new();
            memorySize.Execute();
            WsTestsUtils.DataTests.ContextManager.ContextItem.SaveLogMemory(
                memorySize.GetMemorySizeAppMb(), memorySize.GetMemorySizeFreeMb());
            TestContext.WriteLine($"{nameof(memorySize.GetMemorySizeAppMb)}: {memorySize.GetMemorySizeAppMb()}");
            TestContext.WriteLine($"{nameof(memorySize.GetMemorySizeFreeMb)}: {memorySize.GetMemorySizeFreeMb()}");
            WsTestsUtils.DataTests.AssertGetList<WsSqlLogMemoryModel>(SqlCrudConfigFk, Configurations, false);
        }, false, Configurations);
    }
}