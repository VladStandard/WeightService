// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.PlusFks;

namespace WsStorageCoreTests.TableScaleFkModels.PlusFks;

[TestFixture]
public sealed class PluFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlPluFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluFkModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlPluFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluFkModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}