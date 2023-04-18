// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.PlusBundlesFks;

namespace WsStorageCoreTests.TableScaleFkModels.PlusBundleFks;

[TestFixture]
public sealed class PluBundleFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluBundleFkModel item = WsTestsUtils.DataCore.CreateNewSubstitute<PluBundleFkModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluBundleFkModel item = WsTestsUtils.DataCore.CreateNewSubstitute<PluBundleFkModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}