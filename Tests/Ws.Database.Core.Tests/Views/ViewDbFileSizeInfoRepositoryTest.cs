using Ws.Database.Nhibernate.Entities;
using Ws.Database.Nhibernate.Sessions;
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
        IEnumerable<DbFileSizeInfoEntity> items = DbFileSizeRepository.GetAll();
        foreach (DbFileSizeInfoEntity info in items)
        {
            TestContext.WriteLine($"{info.FileName}: {info.DbFillSize}%");
            Assert.That(info.SizeMb, Is.LessThan(10240));
        }
    }
}