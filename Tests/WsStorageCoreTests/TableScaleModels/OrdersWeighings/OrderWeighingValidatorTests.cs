// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.OrdersWeighings;

[TestFixture]
public sealed class OrderWeighingValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlOrderWeighingModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlOrderWeighingModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlOrderWeighingModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlOrderWeighingModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}