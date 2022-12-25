// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Bundles;

namespace DataCoreTests.Sql.TableScaleModels.Bundles;

internal class BundleValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        // Arrange & Act.
        BundleModel item = DataCore.CreateNewSubstitute<BundleModel>(false);
        // Assert.
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        // Arrange & Act.
        BundleModel item = DataCore.CreateNewSubstitute<BundleModel>(true);
        // Assert.
        DataCore.AssertSqlValidate(item, true);
    }
}

