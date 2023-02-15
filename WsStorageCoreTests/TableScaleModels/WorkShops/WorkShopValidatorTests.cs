// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.WorkShops;

[TestFixture]
internal class WorkShopValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WorkShopModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<WorkShopModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WorkShopModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<WorkShopModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}