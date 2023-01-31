// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.NomenclaturesCharacteristics;

namespace DataCoreTests.Sql.TableScaleModels.NomenclaturesCharacteristics;

[TestFixture]
internal class NomenclaturesCharacteristicsValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        NomenclaturesCharacteristicsModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<NomenclaturesCharacteristicsModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        NomenclaturesCharacteristicsModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<NomenclaturesCharacteristicsModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}