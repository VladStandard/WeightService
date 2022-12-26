// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.ProductSeries;

namespace DataCoreTests.Sql.TableScaleModels.ProductSeries;

[TestFixture]
internal class ProductSeriesModelTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;
    
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCore.AssertSqlPropertyCheckDt<ProductSeriesModel>(nameof(ProductSeriesModel.CreateDt));
        DataCore.AssertSqlPropertyCheckDt<ProductSeriesModel>(nameof(ProductSeriesModel.ChangeDt));
        DataCore.AssertSqlPropertyCheckBool<ProductSeriesModel>(nameof(SqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        DataCore.TableBaseModelAssertToString<ProductSeriesModel>();
    }
    
    [Test]
    public void Model_EqualsNew()
    {
        DataCore.TableBaseModelAssertEqualsNew<ProductSeriesModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        DataCore.TableBaseModelAssertSerialize<ProductSeriesModel>();
    }
}