// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableDiagModels.LogsWebsFks;

namespace WsStorageCoreTests.TableDiagModels.LogsWebsFks;

[TestFixture]
public sealed class LogWebFkSerializeTests
{
    [Test]
    public void Item_Serialize_Validate()
    {
        WsTestsUtils.DataCore.AssertSqlDbContentSerialize<LogWebFkModel>(true);
    }
}