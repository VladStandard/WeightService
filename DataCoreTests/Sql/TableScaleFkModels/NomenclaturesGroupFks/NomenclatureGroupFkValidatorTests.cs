// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.NomenclaturesGroupsFks;

namespace DataCoreTests.Sql.TableScaleFkModels.NomenclaturesGroupFks;

[TestFixture]
internal class NomenclatureGroupFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        NomenclaturesGroupFkModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<NomenclaturesGroupFkModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        NomenclaturesGroupFkModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<NomenclaturesGroupFkModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}