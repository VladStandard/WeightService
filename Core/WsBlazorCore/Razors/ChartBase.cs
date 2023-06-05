// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsBlazorCore.Settings;
using WsStorageCore.Models;
using WsStorageCore.TableScaleModels.Contragents;
using WsStorageCore.Utils;

namespace WsBlazorCore.Razors;

public class ChartBase
{
    #region Public and private fields, properties, constructor

    private BlazorAppSettingsHelper BlazorAppSettings => BlazorAppSettingsHelper.Instance;

    #endregion

    #region Public and private methods

    public string ChartDataFormat(object value) => ((int)value).ToString("####", CultureInfo.InvariantCulture);

    public ChartCountModel[] GetContragentsChartEntities(string field)
    {
        ChartCountModel[] result = Array.Empty<ChartCountModel>();
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            new WsSqlFieldOrderModel { Name = nameof(WsSqlTableBase.CreateDt), Direction = WsSqlOrderDirection.Asc }, WsSqlIsMarked.ShowAll, false);
        WsSqlContragentModel[]? contragents = BlazorAppSettings.AccessManager.AccessList.GetArrayNullable<WsSqlContragentModel>(sqlCrudConfig);
        int i = 0;
        switch (field)
        {
            case nameof(WsSqlTableBase.CreateDt):
                List<ChartCountModel> entitiesDateCreated = new();
                if (contragents?.Any() == true)
                {
                    foreach (WsSqlContragentModel item in contragents)
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
            case nameof(WsSqlTableBase.ChangeDt):
                List<ChartCountModel> entitiesDateModified = new();
                if (contragents?.Any() == true)
                {
                    foreach (WsSqlContragentModel item in contragents)
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