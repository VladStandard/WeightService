using NHibernate.Impl;
using WsStorageCore.Entities.SchemaDiag.Logs;

namespace WsStorageCoreTests.Tables.TableDiagModels.Logs;

[TestFixture]
public sealed class LogRepositoryTests : TableRepositoryTests
{
    private SqlLogRepository LogRepository { get; } = new();

    private SqlLogEntity GetFirstLogModel()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return LogRepository.GetList(SqlCrudConfig).First();
    }

    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(SqlEntityBase.ChangeDt)).Descending;

    [Test]
    public void GetEnumerable()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlLogEntity> items = LogRepository.GetEnumerable(SqlCrudConfig).ToList();
            Assert.That(items.Any(), Is.True);
            ParseRecords(items);
        }, false);
    }

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IList<SqlLogEntity> items = LogRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            ParseRecords(items);
        }, false);
    }

    [Test]
    public void GetItemByUid()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            SqlLogEntity oldLog = GetFirstLogModel();
            SqlLogEntity logByUid = LogRepository.GetItemByUid(oldLog.IdentityValueUid);

            Assert.That(logByUid.IsExists, Is.True);
            Assert.That(logByUid, Is.EqualTo(oldLog));

            TestContext.WriteLine($"Get item success: {logByUid.IdentityValueUid}");
        }, false);
    }

    [Test]
    public void GetItemFirst()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            SqlLogEntity log = LogRepository.GetItemFirst();
            Assert.That(log.IsExists, Is.True);
            TestContext.WriteLine($"{log}");
        }, false);
    }
}