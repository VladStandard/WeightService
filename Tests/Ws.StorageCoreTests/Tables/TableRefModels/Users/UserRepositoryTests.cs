using System.Security.Principal;
using Ws.Database.Core.Entities.Ref.Users;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.StorageCoreTests.Tables.TableRefModels.Users;

[TestFixture]
public sealed class UserRepositoryTests : TableRepositoryTests
{
    private SqlUserRepository UserRepository { get; } = new();

    private string CurrentUser { get; set; }

    public UserRepositoryTests() : base()
    {
        #pragma warning disable CA1416
        CurrentUser = WindowsIdentity.GetCurrent().Name;
        #pragma warning restore CA1416
    }

    [Test, Order(1)]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<UserEntity> items = new SqlUserRepository().GetEnumerable();
            ParseRecords(items);
        });
    }

    [Test, Order(2)]
    public void GetOrCreate()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            UserEntity access = UserRepository.GetItemByNameOrCreate(CurrentUser);
            Assert.That(access.IsExists, Is.True);
            TestContext.WriteLine($"Success created/updated: {access.Name} / {access.IdentityValueUid}");
        });
    }
}