// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.PlusScales;

namespace DataCoreTests.Sql.TableScaleModels.PluScales;

[TestFixture]
internal class PluScaleValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluScaleModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PluScaleModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluScaleModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PluScaleModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}