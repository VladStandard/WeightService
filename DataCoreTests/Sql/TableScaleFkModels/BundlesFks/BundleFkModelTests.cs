// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.BundlesFks;

namespace DataCoreTests.Sql.TableScaleFkModels.BundlesFks;

[TestFixture]
internal class BundleFkModelTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_ToString()
    {
        DataCore.TableBaseModelAssertToString<BundleFkModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCore.TableBaseModelAssertEqualsNew<BundleFkModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        DataCore.TableBaseModelAssertSerialize<BundleFkModel>();
    }
}


