// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.WorkShops;

[TestFixture]
public sealed class WorkShopValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlWorkShopModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlWorkShopModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlWorkShopModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlWorkShopModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}