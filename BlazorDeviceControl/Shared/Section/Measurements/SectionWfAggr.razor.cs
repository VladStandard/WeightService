// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;
using DataCore.Sql;
using DataCore.Sql.DataModels;
using DataCore.Sql.Models;

namespace BlazorDeviceControl.Shared.Section.Measurements;

public partial class SectionWfAggr : BlazorCore.Models.RazorBase
{
    #region Public and private fields, properties, constructor

    private List<WeithingFactSummaryEntity> ItemsCast => Items == null ? new() : Items.Select(x => (WeithingFactSummaryEntity)x).ToList();

    #endregion

    #region Public and private methods

    protected override void OnInitialized()
    {
        base.OnInitialized();
        
        Table = new TableScaleEntity(ProjectsEnums.TableScale.WeithingFacts);
        Items = new();
	}

    protected override void OnParametersSet()
	{
		base.OnParametersSet();
		RunActions(new()
		{
            () =>
            {
                object[] objects = AppSettings.DataAccess.Crud.GetEntitiesNativeObject(
                    SqlQueries.DbScales.Tables.WeithingFacts.GetWeithingFacts(
                        IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0));
                Items = new List<WeithingFactSummaryEntity>().ToList<BaseEntity>();
                foreach (object obj in objects)
                {
                    if (obj is object[] { Length: 5 } item)
                    {
                        Items.Add(new WeithingFactSummaryEntity
                        {
                            WeithingDate = Convert.ToDateTime(item[0]),
                            Count = Convert.ToInt32(item[1]),
                            Scale = item[2] is string scale ? scale : string.Empty,
                            Host = item[3] is string host ? host : string.Empty,
                            Printer = item[4] is string printer ? printer : string.Empty,
                        });
                    }
                }
                ButtonSettings = new(true, true, true, true, true, false, false);
            }
		});
	}

    #endregion
}
