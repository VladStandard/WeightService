using DataCore.Sql.TableScaleFkModels.LogsWebsFks;
namespace BlazorDeviceControl.Razors.SectionComponents.LogReports;

public partial class SectionWebServiceLogs : RazorComponentSectionBase<LogWebFkModel, SqlTableBase>
{
    public SectionWebServiceLogs() : base()
    {
        SqlCrudConfigSection.IsGuiShowFilterMarked = false;
        SqlCrudConfigSection.IsResultShowMarked = true;
        SqlCrudConfigSection.IsResultOrder = true;
        ButtonSettings = new(false, false,  false, false, false, false, false);
    }
    
    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                SqlSectionCast = DataContext.GetListNotNullable<LogWebFkModel>(SqlCrudConfigSection);
                AutoShowFilterOnlyTopSetup();
            }
        });
    }
}