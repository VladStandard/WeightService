// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Enums;
using DataCore.Sql.TableDiagModels.LogsMemories;

namespace WsStorageContextTests.TableDiagModels;

[TestFixture]
internal class LogMemoryContentTests
{
    [Test]
    public void Model_Content_Validate()
    {
        DataCoreTestsUtils.DataCore.AssertSqlDbContentValidate<LogMemoryModel>(true);
    }

    [Test]
    public void DataAccess_GetListSimpleLogsMemories_Exists()
    {
        DataCoreTestsUtils.DataCore.AssertAction(() =>
        {
            List<WsSqlSimpleLogMemory> items = DataCoreTestsUtils.DataCore.DataContext.DataAccess.GetListSimpleLogsMemories(200);
            Assert.IsTrue(items.Any());
            if (!items.Any())
                TestContext.WriteLine($"{nameof(items)} is null or empty!");
            else
            {
                TestContext.WriteLine($"Found {items.Count} items. Print top 5.");
                int i = 0;
                foreach (WsSqlSimpleLogMemory item in items)
                {
                    if (i < 5)
                        TestContext.WriteLine(item);
                    else break;
                    i++;
                }
            }
        }, false, new() { WsConfiguration.DevelopVS, WsConfiguration.ReleaseVS });
    }
}