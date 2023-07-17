// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableScaleModels.PluCharacteristics;

[TestFixture]
public sealed class NomenclaturesCharacteristicsValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlPluCharacteristicModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluCharacteristicModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlPluCharacteristicModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluCharacteristicModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}