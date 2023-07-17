namespace WsStorageCoreTests.Views;

public class ViewRepositoryTests
{
    protected WsSqlCrudConfigModel SqlCrudConfig { get; set; }
    protected List<WsEnumConfiguration> DefaultPublishTypes { get; set; }

    public ViewRepositoryTests()
    {
        SqlCrudConfig = new () { SelectTopRowsCount = 10 };
        DefaultPublishTypes = new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS };
    }
    
}