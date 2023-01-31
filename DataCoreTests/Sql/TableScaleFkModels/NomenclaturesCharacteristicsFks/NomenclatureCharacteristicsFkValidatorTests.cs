// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.NomenclaturesCharacteristicsFks;

namespace DataCoreTests.Sql.TableScaleFkModels.NomenclaturesCharacteristicsFks;

[TestFixture]
internal class NomenclatureCharacteristicsFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        NomenclaturesCharacteristicsFkModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<NomenclaturesCharacteristicsFkModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        NomenclaturesCharacteristicsFkModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<NomenclaturesCharacteristicsFkModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}