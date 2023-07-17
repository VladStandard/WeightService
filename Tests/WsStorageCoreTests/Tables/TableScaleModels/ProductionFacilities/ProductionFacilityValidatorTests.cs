// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableScaleModels.ProductionFacilities;

[TestFixture]
public sealed class ProductionFacilityValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlProductionFacilityModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlProductionFacilityModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlProductionFacilityModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlProductionFacilityModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}