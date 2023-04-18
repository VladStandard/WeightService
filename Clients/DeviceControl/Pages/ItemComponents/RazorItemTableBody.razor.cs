// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Identity;
using WsStorageCore.Enums;
using WsStorageCore.Tables;
using WsStorageCore.Enums;
using WsStorageCore.Tables;

namespace BlazorDeviceControl.Pages.ItemComponents;

public sealed partial class RazorItemTableBody: LayoutComponentBase
{
    #region Public and private fields, properties, constructor
    [Parameter] public WsSqlTableBase SqlItem { get; set; }
    
    private bool IsSqlItem1C => SqlItem is WsSqlTableBase1c;
    
    private string Guid1C => IsSqlItem1C ? $"{((WsSqlTableBase1c?)SqlItem)?.Uid1c}" : $"{Guid.Empty}";
        
    private string IdentityName => 
        SqlItem.Identity.Name switch
        {
            WsSqlFieldIdentity.Id => LocaleCore.Table.Id,
            WsSqlFieldIdentity.Uid => LocaleCore.Table.Uid,
            _ => string.Empty
        };
    
    private string IdentityNameId => 
        SqlItem.Identity.Name switch
        {
            WsSqlFieldIdentity.Id => $"{SqlItem.IdentityValueId}",
            WsSqlFieldIdentity.Uid => $"{SqlItem.IdentityValueUid}",
            _ => $"{Guid.Empty}"
        };
    
    #endregion

    #region Public and private methods

    #endregion
}
