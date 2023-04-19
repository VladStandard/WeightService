// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Bundles;

namespace WsStorageCoreTests.TableScaleModels.Bundles;

[TestFixture]
public sealed class BundleValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        BundleModel item = WsTestsUtils.DataTests.CreateNewSubstitute<BundleModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        BundleModel item = WsTestsUtils.DataTests.CreateNewSubstitute<BundleModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}