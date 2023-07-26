// This is an independent project of an individual developer. Dear PVS-Studio, please check it
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables.TableScaleModels.Printers;
using WsStorageCore.Tables.TableScaleModels.PrintersTypes;

namespace DeviceControl.Pages.Menu.Devices.Printers;

public sealed partial class ItemPrinter : ItemBase<WsSqlPrinterModel>
{
    #region Public and private fields, properties, constructor

    private List<WsSqlPrinterTypeModel> PrinterTypeModels { get; set; }

    #endregion

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        base.SetSqlItemCast();
        PrinterTypeModels = new WsSqlPrinterTypeRepository().GetList(WsSqlCrudConfigFactory.GetCrudConfigActual());
    }

    #endregion
}