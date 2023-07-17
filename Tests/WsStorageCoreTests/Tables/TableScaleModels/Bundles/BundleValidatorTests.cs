// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableScaleModels.Bundles;

[TestFixture]
public sealed class BundleValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlBundleModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlBundleModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlBundleModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlBundleModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}