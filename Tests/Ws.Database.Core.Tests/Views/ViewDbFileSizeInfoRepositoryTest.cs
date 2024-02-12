using Ws.Database.Core.Entities;
using Ws.Database.Core.UnitOfWork;
using Ws.Domain.Models.Entities;

namespace Ws.StorageCoreTests.Views;

[TestFixture]
public sealed class ViewDbFileSizeInfoRepositoryTest : ViewRepositoryTests
{
    private SqlViewDbFileSizeRepository DbFileSizeRepository { get; } = new();

    [Test]
    public void GetList()
    {
        NHibernateHelper.SetSessionFactory();
        List<DbFileSizeInfoEntity> items = DbFileSizeRepository.GetList();
        foreach (DbFileSizeInfoEntity info in items)
        {
            TestContext.WriteLine($"{info.FileName}: {info.DbFillSize}%");
            Assert.That(info.SizeMb, Is.LessThan(10240));
        }
    }
}