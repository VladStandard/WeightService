// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.BarCodes;

namespace DataCoreTests.Sql.TableScaleModels.BarCodes;

[TestFixture]
internal class BarCodeModelTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_ToString()
    {
        DataCore.TableBaseModelAssertToString<BarCodeModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCore.TableBaseModelAssertEqualsNew<BarCodeModel>();
    }
}