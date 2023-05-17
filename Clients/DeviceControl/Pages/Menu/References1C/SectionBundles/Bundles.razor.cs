// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsBlazorCore.Settings;
using WsStorageCore.TableScaleModels.Bundles;

namespace BlazorDeviceControl.Pages.Menu.References1C.SectionBundles;

public sealed partial class Bundles : RazorComponentSectionBase<WsSqlBundleModel>
{
    #region Public and private fields, properties, constructor

    public Bundles() : base()
    {
        ButtonSettings = new ButtonSettingsModel(false, false, true, true, false, false, false);
    }

    #endregion

    #region Public and private methods

    #endregion
}
