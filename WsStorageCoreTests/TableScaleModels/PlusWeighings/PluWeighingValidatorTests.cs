// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.PlusWeighings;

[TestFixture]
internal class PluWeighingValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluWeighingModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PluWeighingModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluWeighingModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PluWeighingModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}