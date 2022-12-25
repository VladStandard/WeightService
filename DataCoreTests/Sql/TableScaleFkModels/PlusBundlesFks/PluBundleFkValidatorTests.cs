// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;

namespace DataCoreTests.Sql.TableScaleFkModels.PlusBundlesFks;

[TestFixture]
internal class PluBundleFkValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        PluBundleFkModel item = DataCore.CreateNewSubstitute<PluBundleFkModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluBundleFkModel item = DataCore.CreateNewSubstitute<PluBundleFkModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}