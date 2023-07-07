// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsBlazorCore.Settings;
using WsStorageCore.TableScaleModels.Clips;

namespace DeviceControl.Pages.Menu.References1C.Clips;

public sealed partial class Clips : SectionBase<WsSqlClipModel>
{
    #region Public and private fields, properties, constructor
    
    public Clips() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }
    
    #endregion
}