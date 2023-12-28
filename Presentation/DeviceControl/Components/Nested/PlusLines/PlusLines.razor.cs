using Ws.StorageCore.Entities.SchemaRef.Lines;
using Ws.StorageCore.Entities.SchemaRef.PlusLines;
using Ws.StorageCore.OrmUtils;

namespace DeviceControl.Components.Nested.PlusLines;

public sealed partial class PlusLines : SectionBase<SqlPluLineEntity>
{
    private SqlPluLineRepository PluLineRepository { get; } = new();
    [Parameter] public SqlLineEntity Line { get; set; }

    public PlusLines() : base()
    {
        ButtonSettings.IsShowMark = false;
        SqlCrudConfigSection.IsResultOrder = true;
    }
    
    protected override void SetSqlSectionCast()
    {
        SqlCrudConfigSection = new() { IsResultOrder = true };
        SqlSectionCast = PluLineRepository.GetListByLine(Line, SqlCrudConfigSection);
    }

    protected override async Task SqlItemNewAsync()
    {
        await DialogService.OpenAsync<AddPlusLines>($"{Line.Name} | ПЛУ", 
        new(){ {"Line", Line} }, 
        new() { Width = "1000px", Height = "700px"});
    }

    protected override async Task SqlItemOpenAsync()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        RouteService.NavigateItemRoute(SqlItemCast.Plu);
    }

    protected override async Task SqlItemOpenNewTabAsync()
    {
        await JsRuntime.InvokeAsync<string>("open", RouteService.GetItemRoute(SqlItemCast.Plu), "_blank");
    }
}
