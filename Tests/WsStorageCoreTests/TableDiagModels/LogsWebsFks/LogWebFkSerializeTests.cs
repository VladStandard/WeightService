// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableDiagModels.LogsWebsFks;

[TestFixture]
public sealed class LogWebFkSerializeTests
{
    [Test]
    public void Item_Serialize_Validate()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentSerialize<WsSqlLogWebFkModel>(true);
    }
}