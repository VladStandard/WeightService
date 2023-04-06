// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using BlazorCore.Settings;

namespace BlazorCore.Razors;

public class ChartBase
{
	#region Public and private fields, properties, constructor

	public BlazorAppSettingsHelper BlazorAppSettings { get; } = BlazorAppSettingsHelper.Instance;

	#endregion

	#region Public and private methods

	public string ChartDataFormat(object value) => ((int)value).ToString("####", CultureInfo.InvariantCulture);

	public ChartCountModel[] GetContragentsChartEntities(string field)
	{
		ChartCountModel[] result = Array.Empty<ChartCountModel>();
		SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            new SqlFieldOrderModel { Name = nameof(SqlTableBase.CreateDt), Direction = WsSqlOrderDirection.Asc }, false, false);
		ContragentModel[]? contragents = BlazorAppSettings.DataAccess.GetArrayNullable<ContragentModel>(sqlCrudConfig);
		int i = 0;
		switch (field)
		{
			case nameof(SqlTableBase.CreateDt):
				List<ChartCountModel> entitiesDateCreated = new();
				if (contragents?.Any() == true)
				{
					foreach (ContragentModel item in contragents)
					{
						entitiesDateCreated.Add(new(item.CreateDt.Date, 1));
						i++;
					}
				}
				IGrouping<DateTime, ChartCountModel>[] entitiesGroupCreated = entitiesDateCreated.GroupBy(item => item.Date).ToArray();
				result = new ChartCountModel[entitiesGroupCreated.Length];
				i = 0;
				foreach (IGrouping<DateTime, ChartCountModel> item in entitiesGroupCreated)
				{
					result[i] = new(item.Key, item.Count());
					i++;
				}
				break;
			case nameof(SqlTableBase.ChangeDt):
				List<ChartCountModel> entitiesDateModified = new();
				if (contragents?.Any() == true)
				{
					foreach (ContragentModel item in contragents)
					{
						entitiesDateModified.Add(new(item.ChangeDt.Date, 1));
						i++;
					}
				}
				IGrouping<DateTime, ChartCountModel>[] entitiesGroupModified = entitiesDateModified.GroupBy(item => item.Date).ToArray();
				result = new ChartCountModel[entitiesGroupModified.Length];
				i = 0;
				foreach (IGrouping<DateTime, ChartCountModel> item in entitiesGroupModified)
				{
					result[i] = new(item.Key, item.Count());
					i++;
				}
				break;
		}
		return result;
	}

	#endregion
}