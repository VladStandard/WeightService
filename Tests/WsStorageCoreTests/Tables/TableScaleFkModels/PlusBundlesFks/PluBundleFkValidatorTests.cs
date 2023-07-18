// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusBundlesFks;

[TestFixture]
public sealed class PluBundleFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlPluBundleFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluBundleFkModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlPluBundleFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluBundleFkModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}