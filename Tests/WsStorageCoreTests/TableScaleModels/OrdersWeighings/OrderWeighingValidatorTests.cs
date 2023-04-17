// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.TableScaleModels.OrdersWeighings;

namespace WsStorageCoreTests.TableScaleModels.OrdersWeighings;

[TestFixture]
internal class OrderWeighingValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        OrderWeighingModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<OrderWeighingModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        OrderWeighingModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<OrderWeighingModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}