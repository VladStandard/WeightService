// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Orders;

namespace DataCoreTests.Sql.TableScaleModels.Orders;

[TestFixture]
internal class OrderValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        // Arrange & Act.
        OrderModel item = DataCore.CreateNewSubstitute<OrderModel>(false);
        // Assert.
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        // Arrange & Act.
        OrderModel item = DataCore.CreateNewSubstitute<OrderModel>(true);
        // Assert.
        DataCore.AssertSqlValidate(item, true);
    }
}
