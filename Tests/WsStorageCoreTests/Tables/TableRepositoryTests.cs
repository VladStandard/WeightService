namespace WsStorageCoreTests.Tables;

public class TableRepositoryTests
{
    protected WsSqlCrudConfigModel SqlCrudConfig { get; set; }
    protected List<WsEnumConfiguration> DefaultPublishTypes { get; set; }

    public TableRepositoryTests()
    {
        SqlCrudConfig = new () { SelectTopRowsCount = 10 };
        DefaultPublishTypes = new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS };
    }
}