// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.TableDiagModels.LogsMemories;

namespace WsStorageContextTests.TableDiagModels;

[TestFixture]
internal class LogMemoryContentTests
{
    [Test]
    public void Model_Content_Validate()
    {
        DataCoreTestsUtils.DataCore.AssertSqlDbContentValidate<LogMemoryModel>(true);
    }
}