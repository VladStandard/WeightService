// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PrintersResourcesFks;

namespace BlazorDeviceControl.Pages.Menu.Devices.SectionPrintersResources;

public sealed partial class PrintersResources : RazorComponentSectionBase<PrinterResourceFkModel>
{
    #region Public and private fields, properties, constructor

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlCrudConfigSection.AddFilters(nameof(PrinterResourceFkModel.Printer), SqlItem);
        base.SetSqlSectionCast();
    }

    #endregion
}