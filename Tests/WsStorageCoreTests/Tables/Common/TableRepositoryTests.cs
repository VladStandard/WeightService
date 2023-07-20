namespace WsStorageCoreTests.Tables.Common;

public class TableRepositoryTests
{
    protected WsSqlCrudConfigModel SqlCrudConfig { get; set; }
    protected List<WsEnumConfiguration> DefaultPublishTypes { get; set; }

    protected WsSqlCrudConfigModel GetNewSqlConfig()
    {
        return new() { SelectTopRowsCount = 10 };
    } 
    
    public TableRepositoryTests()
    {
        SqlCrudConfig = GetNewSqlConfig();
        DefaultPublishTypes = new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS };
    }
}