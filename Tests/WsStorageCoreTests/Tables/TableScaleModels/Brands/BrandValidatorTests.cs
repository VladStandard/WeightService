// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableScaleModels.Brands;

[TestFixture]
public sealed class BrandValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlBrandModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlBrandModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlBrandModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlBrandModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}