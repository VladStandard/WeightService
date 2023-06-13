// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Components.Section;
using WsStorageCore.TableScaleFkModels.PrintersResourcesFks;

namespace DeviceControl.Pages.Menu.Devices.Printers.PrintersResources;

public sealed partial class PrintersResources : SectionBase<WsSqlPrinterResourceFkModel>
{
    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlCrudConfigSection.AddFilters(nameof(WsSqlPrinterResourceFkModel.Printer), SqlItem);
        base.SetSqlSectionCast();
    }

    #endregion
}