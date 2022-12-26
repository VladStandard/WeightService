// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Nomenclatures;

namespace DataCoreTests.Sql.TableScaleModels.Nomenclatures;

[TestFixture]
internal class NomenclatureValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        NomenclatureModel item = DataCore.CreateNewSubstitute<NomenclatureModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        NomenclatureModel item = DataCore.CreateNewSubstitute<NomenclatureModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}
