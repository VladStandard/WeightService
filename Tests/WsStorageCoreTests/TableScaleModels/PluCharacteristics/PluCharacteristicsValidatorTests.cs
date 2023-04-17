// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.TableScaleModels.PlusCharacteristics;

namespace WsStorageCoreTests.TableScaleModels.PluCharacteristics;

[TestFixture]
internal class NomenclaturesCharacteristicsValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluCharacteristicModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PluCharacteristicModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluCharacteristicModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PluCharacteristicModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}