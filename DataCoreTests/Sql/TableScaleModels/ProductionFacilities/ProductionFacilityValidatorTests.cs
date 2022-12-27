// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.ProductionFacilities;

namespace DataCoreTests.Sql.TableScaleModels.ProductionFacilities;

[TestFixture]
internal class ProductionFacilityValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        ProductionFacilityModel item = DataCore.CreateNewSubstitute<ProductionFacilityModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        ProductionFacilityModel item = DataCore.CreateNewSubstitute<ProductionFacilityModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}
