// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.PrintersTypes;

namespace DeviceControl.Pages.Menu.References.SectionPrinterTypes;

public sealed partial class ItemPrinterType : RazorComponentItemBase<WsSqlPrinterTypeModel>
{
    #region Public and private fields, properties, constructor

    //

    #endregion

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNotNullable<WsSqlPrinterTypeModel>(IdentityId);
        if (SqlItemCast.IsNew)
            SqlItemCast = SqlItemNew<WsSqlPrinterTypeModel>();
    }

    #endregion
}
