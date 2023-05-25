// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableDiagModels.LogsWebsFks;

namespace DeviceControl.Pages.Menu.Logs.SectionWebServiceLogs;

public sealed partial class ItemWebServiceLog : RazorComponentItemBase<WsSqlLogWebFkModel>
{
    #region Public and private fields, properties, constructor

    public ItemWebServiceLog() : base()
    {
        ButtonSettings = new(false, false, false, false, false, false, true);
    }

    #endregion

    #region Public and private methods

    #endregion
}
