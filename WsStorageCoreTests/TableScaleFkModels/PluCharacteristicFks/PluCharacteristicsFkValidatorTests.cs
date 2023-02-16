// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleFkModels.PluCharacteristicFks;

[TestFixture]
internal class PluCharacteristicsFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluCharacteristicsFkModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PluCharacteristicsFkModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluCharacteristicsFkModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PluCharacteristicsFkModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}