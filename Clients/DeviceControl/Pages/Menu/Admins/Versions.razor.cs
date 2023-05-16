// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Enums;
using WsStorageCore.TableScaleModels.Versions;

namespace BlazorDeviceControl.Pages.Menu.Admins;

public sealed partial class Versions : RazorComponentSectionBase<WsSqlVersionModel>
{
    #region Public and private fields, properties, constructor

    public Versions() : base()
    {
        SqlCrudConfigSection.IsGuiShowFilterMarked = false;
        SqlCrudConfigSection.AddOrders(new() { Name = nameof(WsSqlVersionModel.Version), Direction = WsSqlOrderDirection.Desc });
        ButtonSettings = new(false, false, false, false, false, false, false);
    }

    #endregion

    #region Public and private methods

    #endregion
}