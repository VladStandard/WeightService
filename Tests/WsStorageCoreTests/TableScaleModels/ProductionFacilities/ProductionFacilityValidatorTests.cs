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
        ProductionFacilityModel item = WsTestsUtils.DataCore.CreateNewSubstitute<ProductionFacilityModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        ProductionFacilityModel item = WsTestsUtils.DataCore.CreateNewSubstitute<ProductionFacilityModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}