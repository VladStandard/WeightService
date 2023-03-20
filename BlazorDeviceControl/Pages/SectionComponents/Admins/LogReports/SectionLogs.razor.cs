// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.LogsTypes;
using DataCore.Sql.Xml;

namespace BlazorDeviceControl.Pages.SectionComponents.Admins.LogReports;

public partial class SectionLogs : RazorComponentSectionBase<LogQuickModel>
{
    #region Public and private fields, properties, constructor
    
    static LogTypeModel LogTypeNew => new();
    public LogTypeModel CurrentType { get; set; }
    private List<LogTypeModel> LogTypes { get; set; }

    public SectionLogs() : base()
    {
        LogTypes = new List<LogTypeModel>();
        LogTypes = DataAccess.GetListNotNullable<LogTypeModel>(new SqlCrudConfigModel());
        CurrentType = LogTypeNew;
    }

    #endregion

    #region Public and private methods

    #endregion
}
