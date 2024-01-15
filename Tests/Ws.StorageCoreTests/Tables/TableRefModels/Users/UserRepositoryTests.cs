using System.Security.Principal;
using Ws.StorageCore.Entities.SchemaRef.Users;

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
            IEnumerable<SqlUserEntity> items = new SqlUserRepository().GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }

    [Test, Order(2)]
    public void GetOrCreate()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            SqlUserEntity access = UserRepository.GetItemByNameOrCreate(CurrentUser);
            Assert.That(access.IsExists, Is.True);
            TestContext.WriteLine($"Success created/updated: {access.Name} / {access.IdentityValueUid}");
        }, false);
    }
}