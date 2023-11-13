﻿using System.Security.Principal;
using WsStorageCore.Entities.SchemaScale.Access;

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
            List<WsSqlAccessEntity> items = AccessRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }

    [Test, Order(2)]
    public void GetOrCreate()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlAccessEntity access = AccessRepository.GetItemByNameOrCreate(CurrentUser);
            WsSqlAccessEntity accessByUid = AccessRepository.GetItemByUid(access.IdentityValueUid);

            Assert.That(access.IsExists, Is.True);
            Assert.That(accessByUid.IsExists, Is.True);

            TestContext.WriteLine($"Success created/updated: {access.Name} / {access.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVs, WsEnumConfiguration.ReleaseVs });
    }

    [Test, Order(3)]
    public void GetByUid()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlAccessEntity accessByName = AccessRepository.GetItemByUsername(CurrentUser);
            Guid uid = accessByName.IdentityValueUid;
            WsSqlAccessEntity accessByUid = AccessRepository.GetItemByUid(uid);

            Assert.That(accessByUid.IsExists, Is.True);
            Assert.That(accessByUid.IdentityValueUid, Is.EqualTo(uid));

            TestContext.WriteLine($"Get item success: {accessByUid.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVs, WsEnumConfiguration.ReleaseVs });
    }

    [Test, Order(4)]
    public void GetNewItem()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlAccessEntity access = AccessRepository.GetNewItem();
            Assert.That(access.IsNew, Is.True);
            TestContext.WriteLine($"New item: {access.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVs, WsEnumConfiguration.ReleaseVs });
    }
}