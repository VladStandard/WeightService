// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.Brands;

[TestFixture]
public sealed class BrandValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        BrandModel item = WsTestsUtils.DataTests.CreateNewSubstitute<BrandModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        BrandModel item = WsTestsUtils.DataTests.CreateNewSubstitute<BrandModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}