// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusStorageMethodsFks;

[TestFixture]
public sealed class PluStorageMethodFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlPluStorageMethodFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluStorageMethodFkModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlPluStorageMethodFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluStorageMethodFkModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}