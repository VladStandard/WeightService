// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.Orders;

[TestFixture]
public sealed class OrderValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlOrderModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlOrderModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlOrderModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlOrderModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}