// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Contragents;

namespace DeviceControl.Pages.Menu.References1C.SectionContragents;

public sealed partial class Contragents : RazorComponentSectionBase<WsSqlContragentModel>
{
    #region Public and private fields, properties, constructor

    public Contragents() : base()
    {
        ButtonSettings = new(false, false, true, true, false, false, false);
    }

    #endregion
}