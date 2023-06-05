// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Components.Section;
using WsBlazorCore.Settings;
using WsStorageCore.ViewScaleModels;

namespace DeviceControl.Pages.Menu.Operations.Barcodes;

public sealed partial class BarCodes : SectionBase<BarcodeView>
{
    #region Public and private fields, properties, constructor

    public BarCodes() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        var query = WsSqlQueriesDiags.Tables.Views.GetBarcodes(
            SqlCrudConfigSection.SelectTopRowsCount, SqlCrudConfigSection.IsMarked
            );
        object[] objects = ContextManager.AccessManager.AccessList.GetArrayObjectsNotNullable(query);
        List<BarcodeView> items = new();
        foreach (var obj in objects)
        {
            if (obj is not object[] { Length: 7 } item)
                continue;
            if (Guid.TryParse(Convert.ToString(item[0]), out var uid))
            {
                items.Add(new BarcodeView
                {
                    IdentityValueUid = uid,
                    IsMarked = Convert.ToBoolean(item[1]),
                    CreateDt = Convert.ToDateTime(item[2]),
                    PluNumber = Convert.ToInt32(item[3]),
                    ValueTop = item[4] as string ?? string.Empty,
                    ValueRight = item[5] as string ?? string.Empty,
                    ValueBottom = item[6] as string ?? string.Empty
                });
            }
        }

        SqlSectionCast = items;
    }

    #endregion
}