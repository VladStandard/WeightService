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
        TestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlLogEntity> items = LogRepository.GetEnumerable(SqlCrudConfig).ToList();
            Assert.That(items.Any(), Is.True);
            ParseRecords(items);
        }, false);
    }

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IList<SqlLogEntity> items = LogRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            ParseRecords(items);
        }, false);
    }

    [Test]
    public void GetItemByUid()
    {
        TestsUtils.DataTests.AssertAction(() =>
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
        TestsUtils.DataTests.AssertAction(() =>
        {
            SqlLogEntity log = LogRepository.GetItemFirst();
            Assert.That(log.IsExists, Is.True);
            TestContext.WriteLine($"{log}");
        }, false);
    }
}