// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.BundlesFks;

namespace DataCoreTests.Sql.TableScaleFkModels.BundlesFks;

[TestFixture]
internal class BundleFkValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        BundleFkModel item = DataCore.CreateNewSubstitute<BundleFkModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        BundleFkModel item = DataCore.CreateNewSubstitute<BundleFkModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}
