// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.Plus;

[TestFixture]
public sealed class PluValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlPluModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlPluModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}