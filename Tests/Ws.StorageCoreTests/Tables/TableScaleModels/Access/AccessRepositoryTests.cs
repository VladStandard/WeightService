using System.Security.Principal;
using Ws.StorageCore.Entities.SchemaScale.Access;

namespace Ws.StorageCoreTests.Tables.TableScaleModels.Access;

[TestFixture]
public sealed class AccessRepositoryTests : TableRepositoryTests
{
    private SqlAccessRepository AccessRepository { get; } = new();

    private string CurrentUser { get; set; }

    public AccessRepositoryTests() : base()
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
            List<SqlAccessEntity> items = AccessRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }

    [Test, Order(2)]
    public void GetOrCreate()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            SqlAccessEntity access = AccessRepository.GetItemByNameOrCreate(CurrentUser);
            SqlAccessEntity accessByUid = AccessRepository.GetItemByUid(access.IdentityValueUid);

            Assert.That(access.IsExists, Is.True);
            Assert.That(accessByUid.IsExists, Is.True);

            TestContext.WriteLine($"Success created/updated: {access.Name} / {access.IdentityValueUid}");
        }, false);
    }

    [Test, Order(3)]
    public void GetByUid()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            SqlAccessEntity accessByName = AccessRepository.GetItemByUsername(CurrentUser);
            Guid uid = accessByName.IdentityValueUid;
            SqlAccessEntity accessByUid = AccessRepository.GetItemByUid(uid);

            Assert.That(accessByUid.IsExists, Is.True);
            Assert.That(accessByUid.IdentityValueUid, Is.EqualTo(uid));

            TestContext.WriteLine($"Get item success: {accessByUid.IdentityValueUid}");
        }, false);
    }
}