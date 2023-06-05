// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Components.Section;
using WsBlazorCore.Settings;
using WsStorageCore.TableScaleModels.Plus;

namespace DeviceControl.Pages.Menu.References1C.Plus;

public sealed partial class Plu : SectionBase<WsSqlPluModel>
{
    #region Public and private fields, properties, constructor

    public Plu() : base()
    {
        SqlCrudConfigSection.IsResultOrder = false;
        SqlCrudConfigSection.AddOrders(
            new() { Name = nameof(WsSqlPluModel.Number), Direction = WsSqlOrderDirection.Asc }
        );
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }

    #endregion

    #region Public and private methods

    #endregion
}