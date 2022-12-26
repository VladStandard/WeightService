// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.ProductionFacilities;

namespace DataCoreTests.Sql.TableScaleModels.ProductionFacilities;

[TestFixture]
internal class ProductionFacilitieModelTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;
    
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCore.AssertSqlPropertyCheckDt<ProductionFacilityModel>(nameof(ProductionFacilityModel.CreateDt));
        DataCore.AssertSqlPropertyCheckDt<ProductionFacilityModel>(nameof(ProductionFacilityModel.ChangeDt));
        DataCore.AssertSqlPropertyCheckBool<ProductionFacilityModel>(nameof(SqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        DataCore.TableBaseModelAssertToString<ProductionFacilityModel>();
    }
    
    [Test]
    public void Model_EqualsNew()
    {
        DataCore.TableBaseModelAssertEqualsNew<ProductionFacilityModel>();
    }
    
    [Test]
    public void Model_Serialize()
    {
        DataCore.TableBaseModelAssertSerialize<ProductionFacilityModel>();
    }
}