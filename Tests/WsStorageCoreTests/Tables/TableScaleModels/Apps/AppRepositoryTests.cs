// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleModels.Apps;

[TestFixture]
public sealed class AppRepositoryTests : TableRepositoryTests
{
    private WsSqlAppRepository AppRepository { get; } = new();

    [Test, Order(1)]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlAppModel> items = AppRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultPublishTypes);
    }

    [Test, Order(2)]
    public void GetOrCreate()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlAppModel access = AppRepository.GetItemByNameOrCreate(nameof(WsStorageCoreTests));
            WsSqlAppModel accessByUid = AppRepository.GetItemByUid(access.IdentityValueUid);
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
            WsSqlAppModel appByName = AppRepository.GetItemByName(nameof(WsStorageCoreTests));
            Guid uid = appByName.IdentityValueUid;
            WsSqlAppModel appByUid = AppRepository.GetItemByUid(uid);

            Assert.That(appByUid.IsExists, Is.True);
            Assert.That(appByUid.IdentityValueUid, Is.EqualTo(uid));

            TestContext.WriteLine($"Get item success: {appByUid.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }

    [Test, Order(4)]
    public void GetNewItem()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlAppModel app = AppRepository.GetNewItem();
            Assert.That(app.IsNotExists, Is.True);
            TestContext.WriteLine($"New item: {app.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}