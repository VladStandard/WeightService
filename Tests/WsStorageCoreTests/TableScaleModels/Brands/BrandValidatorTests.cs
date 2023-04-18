// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Brands;

namespace WsStorageCoreTests.TableScaleModels.Brands;

[TestFixture]
public sealed class BrandValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        BrandModel item = WsTestsUtils.DataCore.CreateNewSubstitute<BrandModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        BrandModel item = WsTestsUtils.DataCore.CreateNewSubstitute<BrandModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}