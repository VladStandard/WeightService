// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.QueriesModels;

namespace BlazorDeviceControl.Razors.Sections.Plus;

public partial class SectionPluWeighingsAggr : RazorBase
{
    #region Public and private fields, properties, constructor

    private List<WeithingFactSummaryModel> ItemsCast
    {
	    get => Items == null ? new() : Items.Select(x => (WeithingFactSummaryModel)x).ToList();
	    set => Items = !value.Any() ? null : new(value);
    }

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
    {
        base.OnInitialized();
        
        Table = new TableScaleEntity(ProjectsEnums.TableScale.PlusWeighings);
        ItemsCast = new();
	}

    protected override void OnParametersSet()
	{
		base.OnParametersSet();

		RunActions(new()
		{
            () =>
            {
                object[] objects = AppSettings.DataAccess.Crud.GetObjects(
                    SqlQueries.DbScales.Tables.WeithingFacts.GetWeithingFacts(
                        IsShowOnlyTop ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0));
                List<WeithingFactSummaryModel> items = new();
                foreach (object obj in objects)
                {
                    if (obj is object[] { Length: 5 } item)
                    {
                        items.Add(new()
                        {
                            WeithingDate = Convert.ToDateTime(item[0]),
                            Count = Convert.ToInt32(item[1]),
                            Scale = item[2] as string ?? string.Empty,
                            Host = item[3] as string ?? string.Empty,
                            Printer = item[4] as string ?? string.Empty,
                        });
                    }
                }
                ItemsCast = items;

				ButtonSettings = new(true, true, true, true, true, false, false);
            }
		});
	}

    #endregion
}
