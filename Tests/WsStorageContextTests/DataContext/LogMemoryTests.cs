// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageContextTests.DataContext;

[TestFixture]
public sealed class LogMemoryTests
{
    private static List<WsConfiguration> Configurations => new() { WsConfiguration.ReleaseVS, WsConfiguration.DevelopVS };
    private static SqlCrudConfigModel SqlCrudConfigFk => new(true, true, false, true, false);

    [Test]
    public void DataContext_GetDbFileSizeInfos_Assert()
    {
        WsTestsUtils.DataCore.AssertAction(() =>
        {
            WsTestsUtils.DataCore.DataContext.DataAccess.SaveLogMemory(1, 1);
            WsTestsUtils.DataCore.AssertGetList<LogMemoryModel>(SqlCrudConfigFk, Configurations, false);
        }, false, Configurations);
    }
}