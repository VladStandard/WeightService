// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.ProductSeries;

[TestFixture]
public sealed class ProductSeriesValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        ProductSeriesModel item = WsTestsUtils.DataTests.CreateNewSubstitute<ProductSeriesModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    { 
        ProductSeriesModel item = WsTestsUtils.DataTests.CreateNewSubstitute<ProductSeriesModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}