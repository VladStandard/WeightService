// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.NomenclaturesGroups;

namespace DataCoreTests.Sql.TableScaleModels.NomenclaturesGroups;

[TestFixture]
internal class NomenclaturesGroupValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        NomenclatureGroupModel item = DataCore.CreateNewSubstitute<NomenclatureGroupModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        NomenclatureGroupModel item = DataCore.CreateNewSubstitute<NomenclatureGroupModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}
