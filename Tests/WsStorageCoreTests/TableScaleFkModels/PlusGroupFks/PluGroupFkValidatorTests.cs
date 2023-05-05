// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleFkModels.PlusGroupFks;

[TestFixture]
public sealed class PluGroupFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlPluGroupFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluGroupFkModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlPluGroupFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluGroupFkModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}