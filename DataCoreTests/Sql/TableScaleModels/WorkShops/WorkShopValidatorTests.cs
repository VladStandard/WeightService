// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.WorkShops;

namespace DataCoreTests.Sql.TableScaleModels.WorkShops;

[TestFixture]
internal class WorkShopValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        // Arrange & Act.
        WorkShopModel item = DataCore.CreateNewSubstitute<WorkShopModel>(false);
        // Assert.
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        // Arrange & Act.
        WorkShopModel item = DataCore.CreateNewSubstitute<WorkShopModel>(true);
        // Assert.
        DataCore.AssertSqlValidate(item, true);
    }
}
