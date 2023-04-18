// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.ProductionFacilities;

namespace WsStorageCoreTests.TableScaleModels.ProductionFacilities;

[TestFixture]
public sealed class ProductionFacilityValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        ProductionFacilityModel item = WsTestsUtils.DataTests.CreateNewSubstitute<ProductionFacilityModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        ProductionFacilityModel item = WsTestsUtils.DataTests.CreateNewSubstitute<ProductionFacilityModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}