using FluentValidation.Results;
using WsDataCore.Serialization;
using WsStorageCore.Utils;

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
    
    protected void ParseRecords<T>(List<T> items) where T : WsSqlTableBase, new()
    {
        Assert.That(items.Any(), Is.True, "No data in database!!!");
            
        TestContext.WriteLine($"Print {items.Count} records.");
            
        foreach (T item in items)
        {
            TestContext.WriteLine(WsSqlQueries.TrimQuery(item.ToString()));
                
            ValidationResult validationResult = WsSqlValidationUtils.GetValidationResult(item, true);
            Assert.That(validationResult.IsValid, Is.True, validationResult.ToString());

            if (item is not SerializeBase sitem)
                continue;

            string xml = WsDataFormatUtils.SerializeAsXmlString<T>(sitem, true, false);
            Assert.IsNotEmpty(xml, "XML is empty");
        }
    }
}