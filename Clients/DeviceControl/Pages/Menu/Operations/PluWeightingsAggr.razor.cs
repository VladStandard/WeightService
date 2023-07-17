// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Views.ViewScaleModels.Aggregations;

namespace DeviceControl.Pages.Menu.Operations;

public sealed partial class PluWeightingsAggr : ComponentBase
{
    #region Public and private fields, properties, constructor
    
    private WsSqlViewWeightingAggrRepository WeightingAggrRepository = WsSqlViewWeightingAggrRepository.Instance;
    private bool IsShowPlu { get; set; }
    private string SqlListCountResult => $"{WsLocaleCore.Strings.ItemsCount}: {PluWeightAggrs.Count:### ### ###}";
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
        PluWeightAggrs = WeightingAggrRepository.GetList(new WsSqlCrudConfigModel());
        StateHasChanged();
    }

    #endregion
}