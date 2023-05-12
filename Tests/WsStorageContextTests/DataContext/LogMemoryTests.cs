// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageContextTests.DataContext;

[TestFixture]
public sealed class LogMemoryTests
{
    private static List<WsEnumConfiguration> Configurations => new() { WsEnumConfiguration.ReleaseVS, WsEnumConfiguration.DevelopVS };
    private static WsSqlCrudConfigModel SqlCrudConfigFk => new(true, true, false, true, false);

    [Test]
    public void DataContext_GetDbFileSizeInfos_Assert()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsTestsUtils.DataTests.ContextManager.ContextItem.SaveLogMemory(1, 1);
            WsTestsUtils.DataTests.AssertGetList<WsSqlLogMemoryModel>(SqlCrudConfigFk, Configurations, false);
        }, false, Configurations);
    }
}