// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Components.Item;
using WsStorageCore.TableScaleModels.PlusScales;

namespace DeviceControl.Pages.Menu.Devices.PlusLines;

public sealed partial class ItemPlusLines : ItemBase<WsSqlPluScaleModel>
{
    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        //TODO: Fix long loading
        base.SetSqlItemCast();
        ContextCache.Load(WsSqlTableName.Plus);
        ContextCache.Load(WsSqlTableName.Lines);
    }

    #endregion
}