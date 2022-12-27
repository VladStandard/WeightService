// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Nomenclatures;

namespace DataCoreTests.Sql.TableScaleModels.NomenclaturesV2;

[TestFixture]
internal class NomenclaturesV2ValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        NomenclatureV2Model item = DataCore.CreateNewSubstitute<NomenclatureV2Model>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        NomenclatureV2Model item = DataCore.CreateNewSubstitute<NomenclatureV2Model>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}
