// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables.TableScaleModels.Versions;

namespace DeviceControl.Pages.Menu.Admins;

public sealed partial class Versions : SectionBase<WsSqlVersionModel>
{
    #region Public and private fields, properties, constructor

    public Versions() : base()
    {
        IsGuiShowFilterMarked = false;
        SqlCrudConfigSection.AddOrders(
            new() { Name = nameof(WsSqlVersionModel.Version), Direction = WsSqlEnumOrder.Desc }
        );
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }

    #endregion
}