// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Enums;
using DataCore.Sql.Models;
using DataCore.Sql.TableDiagModels.LogsMemories;

namespace WsStorageContextTests.DataContext;

[TestFixture]
internal sealed class LogMemoryTests
{
    private static List<Configuration> Configurations => new() { Configuration.ReleaseVS, Configuration.DevelopVS };
    private static SqlCrudConfigModel SqlCrudConfigFk => new(true, true, false, true, false);

    [Test]
    public void DataContext_GetDbFileSizeInfos_Assert()
    {
        DataCoreTestsUtils.DataCore.AssertAction(() =>
        {
            DataCoreTestsUtils.DataCore.DataAccess.LogMemory(1, 1);
            DataCoreTestsUtils.DataCore.AssertGetList<LogMemoryModel>(SqlCrudConfigFk, Configurations, false);
        }, false, Configurations);
    }
}