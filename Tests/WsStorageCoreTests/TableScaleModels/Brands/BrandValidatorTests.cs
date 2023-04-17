// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.TableScaleModels.Brands;

namespace WsStorageCoreTests.TableScaleModels.Brands;

[TestFixture]
internal class BrandValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        BrandModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<BrandModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        BrandModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<BrandModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}