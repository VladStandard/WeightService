// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.TableScaleModels.Bundles;

namespace WsStorageCoreTests.TableScaleModels.Bundles;

[TestFixture]
internal class BundleValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        BundleModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<BundleModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        BundleModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<BundleModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}