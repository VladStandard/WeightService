// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.PlusPackages;

namespace DataCoreTests.Sql.TableScaleModels.PlusPackages;

[TestFixture]
internal class PluPackagesValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        PluPackageModel item = DataCore.CreateNewSubstitute<PluPackageModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluPackageModel item = DataCore.CreateNewSubstitute<PluPackageModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}
