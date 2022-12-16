// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.PrintersResources;

namespace DataCoreTests.Sql.TableScaleModels.PrintersResources;

[TestFixture]
internal class PrinterResourceModelTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_ToString()
    {
        DataCore.TableBaseModelAssertToString<PrinterResourceModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCore.TableBaseModelAssertEqualsNew<PrinterResourceModel>();
    }
}