// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Identity;

namespace BlazorDeviceControl.Pages.ItemComponents;

public sealed partial class RazorItemTableBody: LayoutComponentBase
{
    #region Public and private fields, properties, constructor
    [Parameter] public SqlTableBase SqlItem { get; set; }
    
    private bool IsSqlItem1C => SqlItem is SqlTableBase1c;
    
    private string Guid1C => IsSqlItem1C ? $"{((SqlTableBase1c?)SqlItem)?.Uid1c}" : $"{Guid.Empty}";
        
    private string IdentityName => 
        SqlItem.Identity.Name switch
        {
            SqlFieldIdentity.Id => LocaleCore.Table.Id,
            SqlFieldIdentity.Uid => LocaleCore.Table.Uid,
            _ => string.Empty
        };
    
    private string IdentityNameId => 
        SqlItem.Identity.Name switch
        {
            SqlFieldIdentity.Id => $"{SqlItem.IdentityValueId}",
            SqlFieldIdentity.Uid => $"{SqlItem.IdentityValueUid}",
            _ => $"{Guid.Empty}"
        };
    
    #endregion

    #region Public and private methods

    #endregion
}
