// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemComponents;

public partial class RazorItemTableBody<TItem> : RazorComponentItemBase<TItem> where TItem : SqlTableBase, new()
{
    #region Public and private fields, properties, constructor
    
    private bool IsSqlItem1c => SqlItemCast is SqlTableBase1c;

    #endregion

    #region Public and private methods

    private string? Get1cGuid()
    {
        return IsSqlItem1c ? ((SqlTableBase1c?)SqlItem)?.Uid1c.ToString() : Guid.Empty.ToString();
    }

	#endregion
}
