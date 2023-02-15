// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleFkModels.PlusBundlesFks;

[TestFixture]
internal class PluBundleFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluBundleFkModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PluBundleFkModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluBundleFkModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PluBundleFkModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}