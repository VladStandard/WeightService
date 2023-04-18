using WsStorageCore.TableDiagModels.LogsWebsFks;
using WsStorageCore.TableDiagModels.LogsWebsFks;

namespace BlazorDeviceControl.Pages.Menu.Logs.SectionWebServiceLogs;

public sealed partial class WebServiceLogs : RazorComponentSectionBase<LogWebFkModel>
{

    #region Public and private fields, properties, constructor
    
    public WebServiceLogs() : base()
    {
        SqlCrudConfigSection.IsGuiShowFilterMarked = false;
        SqlCrudConfigSection.IsResultShowMarked = true;
        SqlCrudConfigSection.IsResultOrder = true;
        ButtonSettings = new(false, false,  false, false, false, false, false);
    }
    
    #endregion

    #region Public and private methods
    
    #endregion
}
