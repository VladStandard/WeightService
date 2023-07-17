// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableDiagModels.LogsWebs;

[TestFixture]
public sealed class LogWebValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlLogWebModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlLogWebModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlLogWebModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlLogWebModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}