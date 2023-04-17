// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.TableScaleModels.Bundles;

namespace WsStorageContextTests.TableScaleModels;

[TestFixture]
internal class BundleContentTests
{
    [Test]
    public void Model_Validate_Content()
    {
        DataCoreTestsUtils.DataCore.AssertSqlDbContentValidate<BundleModel>();
    }
}