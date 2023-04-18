// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.PlusWeighingsFks;

namespace WsStorageCoreTests.TableScaleModels.PlusWeighings;

[TestFixture]
public sealed class PluWeighingValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluWeighingModel item = WsTestsUtils.DataCore.CreateNewSubstitute<PluWeighingModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluWeighingModel item = WsTestsUtils.DataCore.CreateNewSubstitute<PluWeighingModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}