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
        WorkShopModel item = DataCore.CreateNewSubstitute<WorkShopModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WorkShopModel item = DataCore.CreateNewSubstitute<WorkShopModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}
