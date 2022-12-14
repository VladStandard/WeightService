// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class NomenclatureCharacteristicsValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        NomenclaturesCharacteristicsModel item = DataCore.CreateNewSubstitute<NomenclaturesCharacteristicsModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        NomenclaturesCharacteristicsModel item = DataCore.CreateNewSubstitute<NomenclaturesCharacteristicsModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}