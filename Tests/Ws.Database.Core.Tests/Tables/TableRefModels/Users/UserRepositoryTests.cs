using System.Security.Principal;
using Ws.Database.Nhibernate.Entities.Ref.Users;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.StorageCoreTests.Tables.TableRefModels.Users;

[TestFixture]
public sealed class UserRepositoryTests : TableRepositoryTests
{
    private SqlUserRepository UserRepository { get; } = new();

    private string CurrentUser { get; }

    public UserRepositoryTests()
    {
        CurrentUser = WindowsIdentity.GetCurrent().Name;
    }

    [Test, Order(1)]
    public void GetList()
    {
        AssertAction(() =>
        {
            IEnumerable<UserEntity> items = new SqlUserRepository().GetAll();
            ParseRecords(items);
        });
    }

    [Test, Order(2)]
    public void GetOrCreate()
    {
        AssertAction(() =>
        {
            UserEntity access = UserRepository.GetItemByUsername(CurrentUser);
            Assert.That(access.IsExists, Is.True);
            TestContext.WriteLine($"Success: {access.Name} / {access.Uid}");
        });
    }
}