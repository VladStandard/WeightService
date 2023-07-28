using System.Security.Principal;
using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleModels.Access;

[TestFixture]
public sealed class AccessRepositoryTests : TableRepositoryTests
{
    private WsSqlAccessRepository AccessRepository { get; } = new();

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
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlAccessModel> items = AccessRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultPublishTypes);
    }

    [Test, Order(2)]
    public void GetOrCreate()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlAccessModel access = AccessRepository.GetItemByNameOrCreate(CurrentUser);
            WsSqlAccessModel accessByUid = AccessRepository.GetItemByUid(access.IdentityValueUid);

            Assert.That(access.IsExists, Is.True);
            Assert.That(accessByUid.IsExists, Is.True);

            TestContext.WriteLine($"Success created/updated: {access.Name} / {access.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }

    [Test, Order(3)]
    public void GetByUid()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlAccessModel accessByName = AccessRepository.GetItemByUsername(CurrentUser);
            Guid uid = accessByName.IdentityValueUid;
            WsSqlAccessModel accessByUid = AccessRepository.GetItemByUid(uid);

            Assert.That(accessByUid.IsExists, Is.True);
            Assert.That(accessByUid.IdentityValueUid, Is.EqualTo(uid));

            TestContext.WriteLine($"Get item success: {accessByUid.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }

    [Test, Order(4)]
    public void GetNewItem()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlAccessModel access = AccessRepository.GetNewItem();
            Assert.That(access.IsNotExists, Is.True);
            TestContext.WriteLine($"New item: {access.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}