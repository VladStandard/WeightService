// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Contragents;

namespace DeviceControl.Pages.Menu.References1C.ContrAgents;

public sealed partial class ContrAgents : RazorComponentSectionBase<WsSqlContragentModel>
{
    #region Public and private fields, properties, constructor

    public ContrAgents() : base()
    {
        ButtonSettings = new(false, false, true, true, false, false, false);
    }

    #endregion
}