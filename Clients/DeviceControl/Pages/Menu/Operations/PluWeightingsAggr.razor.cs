using WsStorageCore.Views.ViewScaleModels.Aggregations;

namespace DeviceControl.Pages.Menu.Operations;

public sealed partial class PluWeightingsAggr : ComponentBase
{
    #region Public and private fields, properties, constructor

    private WsSqlViewWeightingAggrRepository WeightingAggrRepository { get; } = new();
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
        PluWeightAggrs = WeightingAggrRepository.GetList(new());
        StateHasChanged();
    }
    
    public static string GetDayOfWeekRu(DayOfWeek day) => 
        day switch
        {
            DayOfWeek.Monday => "Понедельник",
            DayOfWeek.Tuesday => "Вторник",
            DayOfWeek.Wednesday => "Среда",
            DayOfWeek.Thursday => "Четверг",
            DayOfWeek.Friday => "Пятница",
            DayOfWeek.Saturday => "Суббота",
            DayOfWeek.Sunday => "Воскресенье",
            _ => string.Empty
        };

    #endregion
}
