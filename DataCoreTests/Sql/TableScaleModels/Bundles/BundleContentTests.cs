// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Bundles;

namespace DataCoreTests.Sql.TableScaleModels.Bundles;

[TestFixture]
internal class BundleContentTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_Content()
    {
        DataCore.AssertSqlDbContentValidate<BundleModel>();
    }
}