// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.PlusCharacteristics;

namespace WsStorageCoreTests.TableScaleModels.PluCharacteristics;

[TestFixture]
public sealed class NomenclaturesCharacteristicsValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluCharacteristicModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PluCharacteristicModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluCharacteristicModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PluCharacteristicModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}