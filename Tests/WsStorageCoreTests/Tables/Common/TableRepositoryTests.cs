using FluentValidation.Results;
using NUnit.Framework.Constraints;
using WsDataCore.Serialization;
using WsLocalizationCore.Utils;
using WsStorageCore.Utils;

namespace WsStorageCoreTests.Tables.Common;

public class TableRepositoryTests
{
    protected WsSqlCrudConfigModel SqlCrudConfig { get; set; }
    protected List<WsEnumConfiguration> DefaultPublishTypes { get; set; }

    public TableRepositoryTests()
    {
        DefaultPublishTypes = new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS };
        SqlCrudConfig = new();
    }

    [SetUp]
    public void SetUp()
    {
        SqlCrudConfig = new() { SelectTopRowsCount = 10, IsResultOrder = true };
    }

    protected virtual IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(WsSqlTableBase.Name)).Ascending;

    protected void ParseRecords<T>(List<T> items) where T : WsSqlTableBase, new()
    {
        Assert.That(items.Any(), Is.True, $"{WsLocaleCore.Tests.NoDataInDb}!");
        Assert.That(items, SortOrderValue, $"{WsLocaleCore.Tests.SortingError}!");

        TestContext.WriteLine($"Print {items.Count} records.");

        foreach (T item in items)
        {
            TestContext.WriteLine(WsSqlQueries.TrimQuery(item.ToString()));

            ValidationResult validationResult = WsSqlValidationUtils.GetValidationResult(item, true);
            Assert.That(validationResult.IsValid, Is.True, validationResult.ToString());

            if (item is not SerializeBase sitem)
                continue;

            string xml = WsDataFormatUtils.SerializeAsXmlString<T>(sitem, true, false);
            Assert.That(xml, Is.Not.Empty, "XML is empty");
        }
    }
}