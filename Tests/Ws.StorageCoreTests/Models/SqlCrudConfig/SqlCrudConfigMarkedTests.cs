namespace Ws.StorageCoreTests.Models.WsSqlCrudConfig;

[TestFixture]
public sealed class WsSqlCrudConfigModelMarkedTests
{
    [Test]
    public void CheckShowAll()
    {
        Assert.DoesNotThrow(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = new();

            Assert.That(sqlCrudConfig.IsMarked, Is.EqualTo(SqlEnumIsMarked.ShowAll));
            Assert.That(sqlCrudConfig.Filters, Is.Empty);

            TestContext.WriteLine(sqlCrudConfig);
        });
    }

    [Test]
    public void CheckShowHide()
    {
        Assert.DoesNotThrow(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = new() { IsMarked = SqlEnumIsMarked.ShowOnlyHide };

            Assert.That(sqlCrudConfig.IsMarked, Is.EqualTo(SqlEnumIsMarked.ShowOnlyHide));
            Assert.That(sqlCrudConfig.Filters, Has.Count.EqualTo(1));

            TestContext.WriteLine(sqlCrudConfig);
        });
    }

    [Test]
    public void CheckShowActual()
    {
        Assert.DoesNotThrow(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = new() { IsMarked = SqlEnumIsMarked.ShowOnlyActual };

            Assert.That(sqlCrudConfig.IsMarked, Is.EqualTo(SqlEnumIsMarked.ShowOnlyActual));
            Assert.That(sqlCrudConfig.Filters, Has.Count.EqualTo(1));

            TestContext.WriteLine(sqlCrudConfig);
        });
    }

    [Test]
    public void CheckChangeMarked()
    {
        Assert.DoesNotThrow(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = new() { IsMarked = SqlEnumIsMarked.ShowOnlyActual };

            sqlCrudConfig.IsMarked = SqlEnumIsMarked.ShowOnlyHide;
            Assert.That(sqlCrudConfig.IsMarked, Is.EqualTo(SqlEnumIsMarked.ShowOnlyHide));
            Assert.That(sqlCrudConfig.Filters, Has.Count.EqualTo(1));

            sqlCrudConfig.IsMarked = SqlEnumIsMarked.ShowAll;
            Assert.That(sqlCrudConfig.IsMarked, Is.EqualTo(SqlEnumIsMarked.ShowAll));
            Assert.That(sqlCrudConfig.Filters, Has.Count.EqualTo(0));

            TestContext.WriteLine(sqlCrudConfig);
        });
    }
}