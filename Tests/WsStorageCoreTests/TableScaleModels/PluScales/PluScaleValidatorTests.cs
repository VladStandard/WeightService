// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.PluScales;

[TestFixture]
public sealed class PluScaleValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlPluScaleModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluScaleModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlPluScaleModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluScaleModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}