using WsStorageCore.Entities.SchemaDiag.Logs;

namespace WsStorageCoreTests.Tables.TableDiagModels.Logs;

[TestFixture]
public sealed class LogRepositoryTests : TableRepositoryTests
{
    private WsSqlLogRepository LogRepository { get; } = new();

    private WsSqlLogEntity GetFirstLogModel()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return LogRepository.GetList(SqlCrudConfig).First();
    }

    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(WsSqlEntityBase.ChangeDt)).Descending;

    [Test]
    public void GetEnumerable()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlLogEntity> items = LogRepository.GetEnumerable(SqlCrudConfig).ToList();
            Assert.That(items.Any(), Is.True);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IList<WsSqlLogEntity> items = LogRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }

    [Test]
    public void GetItemByUid()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlLogEntity oldLog = GetFirstLogModel();
            WsSqlLogEntity logByUid = LogRepository.GetItemByUid(oldLog.IdentityValueUid);

            Assert.That(logByUid.IsExists, Is.True);
            Assert.That(logByUid, Is.EqualTo(oldLog));

            TestContext.WriteLine($"Get item success: {logByUid.IdentityValueUid}");
        }, false, DefaultConfigurations);
    }

    [Test]
    public void GetItemFirst()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlLogEntity log = LogRepository.GetItemFirst();
            Assert.That(log.IsExists, Is.True);
            TestContext.WriteLine($"{log}");
        }, false, DefaultConfigurations);
    }
}