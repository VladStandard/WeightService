// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.PrintersTypes;

namespace DataCoreTests.Sql.TableScaleModels.PrintersTypes;

[TestFixture]
internal class PrinterTypeModelTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;
    
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCore.AssertSqlPropertyCheckDt<PrinterTypeModel>(nameof(PrinterTypeModel.CreateDt));
        DataCore.AssertSqlPropertyCheckDt<PrinterTypeModel>(nameof(PrinterTypeModel.ChangeDt));
        DataCore.AssertSqlPropertyCheckBool<PrinterTypeModel>(nameof(SqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        DataCore.TableBaseModelAssertToString<PrinterTypeModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCore.TableBaseModelAssertEqualsNew<PrinterTypeModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        DataCore.TableBaseModelAssertSerialize<PrinterTypeModel>();
    }       
}   