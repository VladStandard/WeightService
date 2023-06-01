// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Components.Section;
using WsBlazorCore.Settings;
using WsStorageCore.ViewScaleModels;

namespace DeviceControl.Pages.Menu.Operations.PlusLabels;

public sealed partial class PlusLabels : SectionBase<PlusLabelView>
{
	#region Public and private fields, properties, constructor
    
    public PlusLabels() :base()
	{
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        var query = WsSqlQueriesDiags.Tables.Views.GetPluLabels(
                SqlCrudConfigSection.SelectTopRowsCount,
                SqlCrudConfigSection.IsMarked);
        object[] objects = ContextManager.AccessManager.AccessList.GetArrayObjectsNotNullable(query);
        List<PlusLabelView> items = new();
        foreach (var obj in objects)
        {
            if (obj is not object[] { Length: 10 } item)
                continue;
            if (Guid.TryParse(Convert.ToString(item[0]), out var uid))
            {
                items.Add(new PlusLabelView
                {
                    IdentityValueUid = uid,
                    CreateDt = Convert.ToDateTime(item[1]),
                    IsMarked = Convert.ToBoolean(item[2]),
                    ProductDate = Convert.ToDateTime(item[3]),
                    ExpirationDate = Convert.ToDateTime(item[4]),
                    WeightingDate = Convert.ToDateTime(item[5]),
                    Line = item[6] as string ?? string.Empty,
                    PluNumber = Convert.ToInt32(item[7]),
                    PluName = item[8] as string ?? string.Empty,
                    Template = item[9] as string ?? string.Empty
                });
            }
        }

        SqlSectionCast = items;
    }

    #endregion
}
