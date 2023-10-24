using WsStorageCore.Views.ViewPrintModels.PluLabelAggr;
namespace DeviceControl.Pages.Menu.Operations;

public sealed partial class PluLabelsAggr : ComponentBase
{
    #region Public and private fields, properties, constructor

    private WsSqlViewPluLabelAggrRepository PluLabelAggrRepository { get; } = new();
    private string SqlListCountResult => $"{WsLocaleCore.Strings.ItemsCount}: {PluWeightAggrs.Count:### ### ###}";
    private string GetAverageCountResult => $"Средняя производительность: {GetAverageCount():### ### ###}";
    private List<WsSqlViewPluLabelAggrModel> PluWeightAggrs { get; set; }
    
    private int GetAverageCount()
    {
        if (PluWeightAggrs.Count == 0) return 0;
        return PluWeightAggrs.Sum(item => item.TotalCount) / PluWeightAggrs.Count;
    } 
    
    public PluLabelsAggr()
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
        PluWeightAggrs = PluLabelAggrRepository.GetList(new());
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
