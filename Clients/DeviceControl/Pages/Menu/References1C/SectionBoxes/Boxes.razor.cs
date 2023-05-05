// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsBlazorCore.Settings;
using WsStorageCore.TableScaleModels.Boxes;

namespace BlazorDeviceControl.Pages.Menu.References1C.SectionBoxes;

public sealed partial class Boxes : RazorComponentSectionBase<WsSqlBoxModel>
{
    #region Public and private fields, properties, constructor

    public Boxes() : base()
    {
        ButtonSettings = new ButtonSettingsModel(false, false, true, true, false, false, false);
    }

    #endregion

    #region Public and private methods

    #endregion
}