// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.DeviceScalesFks;
using WsStorageCore.ViewScaleModels;

namespace BlazorDeviceControl.Pages.Menu.Operations.SectionPlusWeighings;

public sealed partial class PluWeightings : RazorComponentSectionBase<PluWeightingView>
{
    #region Public and private fields, properties, constructor

    private List<DeviceScaleFkModel> DeviceScaleFk { get; set; }

    public PluWeightings() : base()
	{
        ButtonSettings = new(false, true, false, true, false, false, false);
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        var query = WsSqlQueriesDiags.Tables.Views.GetPluWeightings(SqlCrudConfigSection.IsResultShowOnlyTop
            ? ContextManager.JsonSettings.Local.SelectTopRowsCount
            : 0, SqlCrudConfigSection.IsResultShowMarked);
        object[] objects = ContextManager.AccessManager.AccessList.GetArrayObjectsNotNullable(query);
        List<PluWeightingView> items = new();
        foreach (var obj in objects)
        {
            if (obj is not object[] { Length: 8 } item)
                continue;
            if (Guid.TryParse(Convert.ToString(item[0]), out var uid))
            {
                items.Add(new PluWeightingView
                {
                    IdentityValueUid = uid,
                    IsMarked = Convert.ToBoolean(item[1]),
                    CreateDt = Convert.ToDateTime(item[2]),
                    Line = item[3] as string ?? string.Empty,
                    PluNumber = Convert.ToInt32(item[4]),
                    PluName = item[5] as string ?? string.Empty,
                    TareWeight = Convert.ToDecimal(item[6]),
                    NettoWeight = Convert.ToDecimal(item[7])
                });
            }
        }
        SqlSectionCast = items;
    }

    #endregion
}