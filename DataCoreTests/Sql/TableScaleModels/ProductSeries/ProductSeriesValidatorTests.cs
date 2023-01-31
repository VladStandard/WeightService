// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.ProductSeries;

namespace DataCoreTests.Sql.TableScaleModels.ProductSeries;

[TestFixture]
internal class ProductSeriesValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        ProductSeriesModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<ProductSeriesModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    { 
        ProductSeriesModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<ProductSeriesModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}