// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableScaleModels.Tasks;

[TestFixture]
public sealed class TaskValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlTaskModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlTaskModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlTaskModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlTaskModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}