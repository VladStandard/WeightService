// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Components.Item;

public sealed partial class ItemTableBody : ComponentBase
{
    #region Public and private fields, properties, constructor

    [Parameter] public WsSqlTableBase SqlItem { get; set; }

    private bool IsSqlItem1C => SqlItem is WsSqlTable1CBase;

    private string Guid1C => IsSqlItem1C && SqlItem is WsSqlTable1CBase sqlTable && sqlTable.Uid1C != Guid.Empty
        ? $"{sqlTable.Uid1C}"
        : "Нет в базе 1С";

    private string IdentityName =>
        SqlItem.Identity.Name switch
        {
            WsSqlEnumFieldIdentity.Id => WsLocaleCore.Table.Id,
            WsSqlEnumFieldIdentity.Uid => WsLocaleCore.Table.Uid,
            _ => string.Empty
        };

    private string IdentityNameId =>
        SqlItem.Identity.Name switch
        {
            WsSqlEnumFieldIdentity.Id => SqlItem.IdentityValueId != 0 ? $"{SqlItem.IdentityValueId}" : "Новая запись",
            WsSqlEnumFieldIdentity.Uid => SqlItem.IdentityValueUid != Guid.Empty ? $"{SqlItem.IdentityValueUid}" : "Новая запись",
            _ => "Новая запись"
        };

    #endregion
}
