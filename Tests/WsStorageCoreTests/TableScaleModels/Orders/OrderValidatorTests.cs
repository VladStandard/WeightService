// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.TableScaleModels.Orders;

namespace WsStorageCoreTests.TableScaleModels.Orders;

[TestFixture]
internal class OrderValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        OrderModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<OrderModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        OrderModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<OrderModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}