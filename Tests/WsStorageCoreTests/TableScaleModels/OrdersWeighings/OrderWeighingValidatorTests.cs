// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.OrdersWeighings;

namespace WsStorageCoreTests.TableScaleModels.OrdersWeighings;

[TestFixture]
public sealed class OrderWeighingValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        OrderWeighingModel item = WsTestsUtils.DataCore.CreateNewSubstitute<OrderWeighingModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        OrderWeighingModel item = WsTestsUtils.DataCore.CreateNewSubstitute<OrderWeighingModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}