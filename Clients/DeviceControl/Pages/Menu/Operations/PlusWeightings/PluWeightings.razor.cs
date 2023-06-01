// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Components.Section;
using WsBlazorCore.Settings;
using WsStorageCore.TableScaleFkModels.DeviceScalesFks;
using WsStorageCore.ViewScaleModels;

namespace DeviceControl.Pages.Menu.Operations.PlusWeightings;

public sealed partial class PluWeightings : SectionBase<PluWeightingView>
{
    #region Public and private fields, properties, constructor

    private List<WsSqlDeviceScaleFkModel> DeviceScaleFk { get; set; }

    public PluWeightings() : base()
	{
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        var query = WsSqlQueriesDiags.Tables.Views.GetPluWeightings(
            SqlCrudConfigSection.SelectTopRowsCount, SqlCrudConfigSection.IsMarked);
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
