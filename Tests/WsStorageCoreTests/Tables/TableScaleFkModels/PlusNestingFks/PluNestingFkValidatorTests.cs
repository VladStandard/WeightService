// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables.TableScaleFkModels.PlusNestingFks;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusNestingFks;

[TestFixture]
public sealed class PluNestingFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlPluNestingFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluNestingFkModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlPluNestingFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluNestingFkModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}