// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.TableScaleModels.Logs;
using DataCore.Sql.TableScaleModels.LogsTypes;

namespace BlazorDeviceControl.Razors.SectionComponents.LogReports;

public partial class SectionLogsInformation : RazorComponentSectionBase<LogModel, LogTypeModel>
{
    #region Public and private fields, properties, constructor

    public SectionLogsInformation()
    {
	    SqlCrudConfigSection.IsGuiShowItemsCount = true;
        SqlCrudConfigSection.IsGuiShowFilterMarked = true;
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
            () =>
            {
	            SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
		            nameof(LogTypeModel.Number), (byte)LogTypeEnum.Information,
		            SqlCrudConfigItem.IsResultShowMarked, SqlCrudConfigItem.IsResultShowOnlyTop);
				SqlItem = DataAccess.GetItemNotNullable<LogTypeModel>(sqlCrudConfig);
            }
		});
	}

    #endregion
}
