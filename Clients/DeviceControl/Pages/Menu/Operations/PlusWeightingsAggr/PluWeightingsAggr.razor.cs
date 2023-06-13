// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsLocalizationCore.Utils;
using WsStorageCore.Helpers;
using WsStorageCore.TableScaleFkModels.Aggregations;

namespace DeviceControl.Pages.Menu.Operations.PlusWeightingsAggr;

public sealed partial class PluWeightingsAggr : LayoutComponentBase
{
    #region Public and private fields, properties, constructor

    private bool IsShowPlu { get; set; }

    private string SqlListCountResult =>
        $"{WsLocaleCore.Strings.ItemsCount}: {PluWeightAggrs.Count:### ### ###}";

    private List<WsSqlViewWeightingAggrModel> PluWeightAggrs { get; set; }

    public PluWeightingsAggr()
    {
        PluWeightAggrs = new();
    }

    #endregion

    #region Public and private methods

    protected override void OnAfterRender(bool firstRender)
    {
        if (!firstRender)
        {
            base.OnAfterRender(firstRender);
            return;
        }

        GetSectionData();
    }

    private void GetSectionData()
    {
        PluWeightAggrs = WsSqlContextViewHelper.Instance.GetListViewWeightingsAggr(200);
        StateHasChanged();
    }

    #endregion
}