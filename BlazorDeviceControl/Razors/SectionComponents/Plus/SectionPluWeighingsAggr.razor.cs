// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.Xml;

namespace BlazorDeviceControl.Razors.SectionComponents.Plus;

public partial class SectionPluWeighingsAggr : RazorComponentSectionBase<WeithingFactSummaryModel, SqlTableBase>
{
    #region Public and private fields, properties, constructor

    public SectionPluWeighingsAggr()
    {
		RazorComponentConfig.IsShowItemsCount = true;
    }

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
            () =>
            {
                object[] objects = DataAccess.GetArrayObjectsNotNull(
                    SqlQueries.DbScales.Tables.WeithingFacts.GetWeithingFacts(
                        RazorComponentConfig.IsShowOnlyTop ? DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0));
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
                SqlItemsCast = items;

				ButtonSettings = new(false, false, false, false, false, false, false);
            }
		});
	}

    #endregion
}
