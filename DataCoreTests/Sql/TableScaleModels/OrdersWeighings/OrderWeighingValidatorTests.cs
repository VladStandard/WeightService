// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.OrdersWeighings;

namespace DataCoreTests.Sql.TableScaleModels.OrdersWeighings;

[TestFixture]
internal class OrderWeighingValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        // Arrange & Act.
        OrderWeighingModel item = DataCore.CreateNewSubstitute<OrderWeighingModel>(false);
        // Assert.
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        // Arrange & Act.
        OrderWeighingModel item = DataCore.CreateNewSubstitute<OrderWeighingModel>(true);
        // Assert.
        DataCore.AssertSqlValidate(item, true);
    }
}
