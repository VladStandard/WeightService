using DataCore.Sql.TableDiagModels.LogsWebsFks;

namespace BlazorDeviceControl.Pages.SectionComponents.Admins.LogReports;

public sealed partial class SectionWebServiceLogs : RazorComponentSectionBase<LogWebFkModel>
{

    #region Public and private fields, properties, constructor
    
    public SectionWebServiceLogs() : base()
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
