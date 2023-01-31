// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.NomenclaturesGroupsFks;

namespace DataCoreTests.Sql.TableScaleFkModels.NomenclaturesGroupFks;

[TestFixture]
internal class NomenclatureGroupFkValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        NomenclaturesGroupFkModel item = DataCore.CreateNewSubstitute<NomenclaturesGroupFkModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        NomenclaturesGroupFkModel item = DataCore.CreateNewSubstitute<NomenclaturesGroupFkModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}