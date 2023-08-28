namespace WsStorageCoreTestsDbContext.DataContext;

[TestFixture]
public sealed class LogMemoryTests
{
    private static List<WsEnumConfiguration> Configurations =>
        new() { WsEnumConfiguration.ReleaseVS, WsEnumConfiguration.DevelopVS };

    [Test]
    public void DataContext_GetDbFileSizeInfos_Assert()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            MemorySizeModel memorySize = new();
            memorySize.Execute();
            WsTestsUtils.DataTests.ContextManager.LogMemoryRepository.Save(
                memorySize.GetMemorySizeAppMb(), memorySize.GetMemorySizeFreeMb()
            );
            TestContext.WriteLine($"{nameof(memorySize.GetMemorySizeAppMb)}: {memorySize.GetMemorySizeAppMb()}");
            TestContext.WriteLine($"{nameof(memorySize.GetMemorySizeFreeMb)}: {memorySize.GetMemorySizeFreeMb()}");
        }, false, Configurations);
    }
}