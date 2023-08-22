// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Pages.Menu.Logs.WebServiceLogs;

public sealed partial class ItemWebServiceLog : ItemBase<WsSqlLogWebFkModel>
{
    #region Public and private fields, properties, constructor

    public ItemWebServiceLog() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }

    #endregion
}
