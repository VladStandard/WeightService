// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.WorkShops;

[TestFixture]
public sealed class WorkShopValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WorkShopModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WorkShopModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WorkShopModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WorkShopModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}