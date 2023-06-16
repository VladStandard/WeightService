// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Components.Item;
using WsStorageCore.TableScaleFkModels.PrintersResourcesFks;
using WsStorageCore.TableScaleModels.Printers;
using WsStorageCore.TableScaleModels.TemplatesResources;

namespace DeviceControl.Pages.Menu.Devices.Printers.PrintersResources;

public sealed partial class ItemPrinterResources : ItemBase<WsSqlPrinterResourceFkModel>
{
    #region Public and private fields, properties, constructor

    private List<WsSqlPrinterModel> PrinterModels { get; set; }

    private List<WsSqlTemplateResourceModel> TemplateResourceModels { get; set; }

    #endregion

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        SqlItemCast = ContextManager.SqlCoreManager.SqlCore.GetItemNotNullable<WsSqlPrinterResourceFkModel>(Id);
        if (SqlItemCast.IsNew)
            SqlItemCast = SqlItemNewEmpty<WsSqlPrinterResourceFkModel>();
        PrinterModels = ContextManager.SqlCoreManager.SqlCoreList.GetListNotNullable<WsSqlPrinterModel>(WsSqlCrudConfigUtils
            .GetCrudConfigComboBox());
        TemplateResourceModels = ContextManager.SqlCoreManager.SqlCoreList.GetListNotNullable<WsSqlTemplateResourceModel>(WsSqlCrudConfigUtils
            .GetCrudConfigComboBox());
    }

    #endregion
}