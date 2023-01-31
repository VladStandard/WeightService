// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Nomenclatures;

namespace DataCoreTests.Sql.TableScaleModels.Nomenclatures;

[TestFixture]
internal class NomenclatureValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        NomenclatureModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<NomenclatureModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        NomenclatureModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<NomenclatureModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}