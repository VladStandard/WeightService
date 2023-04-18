// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.WorkShops;

namespace WsStorageCoreTests.TableScaleModels.WorkShops;

[TestFixture]
public sealed class WorkShopValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WorkShopModel item = WsTestsUtils.DataCore.CreateNewSubstitute<WorkShopModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WorkShopModel item = WsTestsUtils.DataCore.CreateNewSubstitute<WorkShopModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}