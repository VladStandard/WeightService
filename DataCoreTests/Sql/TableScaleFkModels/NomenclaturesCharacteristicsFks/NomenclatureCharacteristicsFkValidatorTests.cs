// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.NomenclaturesCharacteristicsFks;

namespace DataCoreTests.Sql.TableScaleFkModels.NomenclaturesCharacteristicsFks;

[TestFixture]
internal class NomenclatureCharacteristicsFkValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        NomenclaturesCharacteristicsFkModel item = DataCore.CreateNewSubstitute<NomenclaturesCharacteristicsFkModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        NomenclaturesCharacteristicsFkModel item = DataCore.CreateNewSubstitute<NomenclaturesCharacteristicsFkModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}