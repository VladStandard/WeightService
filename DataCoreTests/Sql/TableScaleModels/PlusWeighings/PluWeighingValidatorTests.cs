// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.PlusWeighings;

namespace DataCoreTests.Sql.TableScaleModels.PlusWeighings;

[TestFixture]
internal class PluWeighingValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        PluWeighingModel item = DataCore.CreateNewSubstitute<PluWeighingModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluWeighingModel item = DataCore.CreateNewSubstitute<PluWeighingModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}
