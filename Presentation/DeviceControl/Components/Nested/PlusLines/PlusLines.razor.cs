using WsStorageCore.OrmUtils;
namespace DeviceControl.Components.Nested.PlusLines;

public sealed partial class PlusLines : SectionBase<WsSqlPluScaleEntity>
{
    #region Public and private fields, properties, constructor

    private WsSqlPluLineRepository PluLineRepository { get; } = new();
    [Parameter] public WsSqlScaleEntity Scale { get; set; }

    public PlusLines() : base()
    {
        ButtonSettings.IsShowMark = false;
        SqlCrudConfigSection.IsResultOrder = true;
        SqlCrudConfigSection.AddFilter(SqlRestrictions.Equal(nameof(WsSqlPluScaleEntity.IsActive), true));
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = PluLineRepository.GetListByLine(Scale, SqlCrudConfigSection);
    }

    protected override async Task SqlItemNewAsync()
    {
        await DialogService.OpenAsync<AddPlusLines>($"{Scale.Description} | ПЛУ", 
        new(){ {"Line", Scale} }, 
        new() { Width = "1000px", Height = "700px"});
    }
    protected override async Task SqlItemDeleteAsync()
    {
        SqlItemCast.IsActive = false;
        await base.SqlItemDeleteAsync();
    }

    protected override async Task SqlItemOpenAsync()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        RouteService.NavigateItemRoute(SqlItemCast.Plu);
    }

    protected override async Task SqlItemOpenNewTabAsync()
    {
        await JsRuntime.InvokeAsync<string>("open", WsRouteService.GetItemRoute(SqlItemCast.Plu), "_blank");
    }

    #endregion
}
